using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Microsoft.TeamFoundation.Discussion.Client;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.Office.Interop;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace CheckinReviewReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeProjectCombo();
        }

        public void InitializeProjectCombo()
        {
            TfsTeamProjectCollection projectCollection = GetProjectCollections();
            WorkItemStore workItemStore = projectCollection.GetService<WorkItemStore>();
           
            foreach (Project pr in workItemStore.Projects)
            {
                comboBox1.Items.Add(pr.Name);
            }
        }
        private void btnGenRpt_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            TeamFoundationDiscussionService service = new TeamFoundationDiscussionService();

            string project = null;

            string path  = ConfigurationManager.AppSettings["Path"];

            if (comboBox1.SelectedItem == null || comboBox1.SelectedItem.ToString() == "")
            {
                project = ConfigurationManager.AppSettings["Project"];
            }
            else
            {
                project = comboBox1.SelectedItem.ToString();
            }


            Microsoft.TeamFoundation.Client.TfsTeamProjectCollection projectCollection = GetProjectCollections();
            service.Initialize(projectCollection);
            IDiscussionManager discussionManager = service.CreateDiscussionManager();
            
            List<CodeReviewComment> comments = new List<CodeReviewComment>();

            VersionControlServer vcs = projectCollection.GetService<VersionControlServer>();

         
            
            var changesets = vcs.QueryHistory("$\\" + project + path , VersionSpec.Latest, 0, RecursionType.Full, "", null, null, int.MaxValue, false, false);
            
            foreach (Changeset cs in changesets)
            {
                var changeSetIds = cs.ChangesetId;
                var changeSetComments = cs.Comment;
                var changeSetAuthors = cs.Committer;
                var workItemCollection = cs.WorkItems;
                

                foreach (WorkItem wrkItem in workItemCollection)
                {
                    int workItemId = wrkItem.Id;
                    var reviewr = wrkItem.CreatedBy;
                    var reviewrComment = wrkItem.Description;
                    IAsyncResult result = discussionManager.BeginQueryByCodeReviewRequest(workItemId, QueryStoreOptions.ServerAndLocal, new AsyncCallback(CallCompletedCallback), null);
                    var output = discussionManager.EndQueryByCodeReviewRequest(result);

                    foreach (DiscussionThread thread in output)
                    {
                        if (thread.RootComment != null)
                        {
                            CodeReviewComment comment = new CodeReviewComment();
                            comment.Author = reviewr;
                            comment.AuthorComment = reviewrComment;
                            comment.PublishDate = thread.RootComment.PublishedDate.ToShortDateString();
                            comment.ItemName = thread.ItemPath;
                            comment.ChangesetIds = changeSetIds.ToString();
                            comment.ChangeSetComments = changeSetComments;
                            comment.ChangeSetCommitter = changeSetAuthors;
                            comment.ReviewWorkItem = workItemId.ToString();
                            comment.WorkItemState = wrkItem.State;
                            comment.Reviewer = thread.RootComment.Author.DisplayName;
                            comment.ReviewComments = thread.RootComment.Content; 
                            comments.Add(comment);
                        }
                    }
                }
            }

            DataTable dataTable = ConvertToDataTable(comments);

            GenerateExcel(dataTable,project);
        }
        static void CallCompletedCallback(IAsyncResult result)
        {
            // For Error Handling
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
            TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public void GenerateExcel(DataTable dt,string project)
        {
            int colcnt = dt.Columns.Count;
            int rowcnt = dt.Rows.Count;
            string ExcelFilePath = ConfigurationManager.AppSettings["FilePath"] + Guid.NewGuid().ToString() +"_"+ project + "_CheckinReviewReport";
            Excel.Application xlsApp = new Excel.Application();
            Excel.Workbook wrkBook = xlsApp.Workbooks.Add();
            Excel.Worksheet wrksheet = wrkBook.ActiveSheet;
            wrksheet.Columns.WrapText = true;
            

            // column headings
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                wrksheet.Cells[1, (i + 1)] = dt.Columns[i].ColumnName;
            }

            // rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // to do: format datetime values before printing
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    wrksheet.Cells[(i + 2), (j + 1)] = dt.Rows[i][j];                    

                }
            }

            // check fielpath
            if (ExcelFilePath != null && ExcelFilePath != "")
            {
                try
                {
                    wrksheet.SaveAs(ExcelFilePath);
                    xlsApp.Quit();
                    MessageBox.Show("Excel file saved!");
                }
                catch (Exception ex)
                {
                    throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                        + ex.Message);
                }
            }
        }

        public TfsTeamProjectCollection GetProjectCollections()
        {
            Uri uri = new Uri(@"http://otb-tfs:8080/tfs/test");
            
            string userid = ConfigurationManager.AppSettings["UserName"];
            string password = ConfigurationManager.AppSettings["Password"];
            
            System.Net.NetworkCredential netCred = new System.Net.NetworkCredential(userid, password, "ODESSAPDC01");
            VssBasicCredential bsCred = new VssBasicCredential(netCred);
            Microsoft.VisualStudio.Services.Common.WindowsCredential winCred = new Microsoft.VisualStudio.Services.Common.WindowsCredential(netCred);
            VssCredentials vssCred = new VssClientCredentials(winCred);
            vssCred.PromptType = CredentialPromptType.DoNotPrompt;


            Microsoft.TeamFoundation.Client.TfsTeamProjectCollection projectCollection = new TfsTeamProjectCollection(uri, vssCred);
            projectCollection.EnsureAuthenticated();
            return projectCollection;
        }
    }
}

