using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace SendMailApp
{
    class SendMail
    {
        static void Main(string[] args)
        {
            try
            {

                //Mailing List
                List<string> lstAllRecipients = new List<string>();
                lstAllRecipients.Add("jairajan.padinharammadathil@odessainc.com");

                string OutlookFilepath = "C:\\Program Files\\Microsoft Office\\root\\Office16\\OUTLOOK.exe";

                Outlook.Application outlookApp = new Outlook.Application();
                Outlook._MailItem oMailItem = (Outlook._MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                Outlook.Inspector oInspector = oMailItem.GetInspector;
                Outlook.NameSpace nsp = outlookApp.GetNamespace("MAPI");


                // Recipient
                Outlook.Recipients oRecips = (Outlook.Recipients)oMailItem.Recipients;
                foreach (String recipient in lstAllRecipients)
                {
                    Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(recipient);
                    oRecip.Resolve();
                }

                //Add CC
               // Outlook.Recipient oCCRecip = oRecips.Add("Giridhara.P@odessainc.com");
                //oCCRecip.Type = (int)Outlook.OlMailRecipientType.olCC;
                //oCCRecip.Resolve();

                //Add Subject
                oMailItem.Subject = "Test Mail";

                oMailItem.BodyFormat = Outlook.OlBodyFormat.olFormatHTML;

                // Add Body
                oMailItem.Body = "Test Mail Body";

                //Send the mail
                oMailItem.Send();

                Process process = new Process();
                process.StartInfo = new ProcessStartInfo(OutlookFilepath);
                process.Start();

                //nsp.Logon(false, false, false, true);
                //nsp.Logoff();


                //if (Process.GetProcessesByName("OUTLOOK").Count() > 0)
                //{

                //    // Check whether there is an Outlook process running
                //    // If so, use the GetActiveObject method to obtain the application process
                //    var app = System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application") as Outlook.Application;

                //    // use the Quit method to close the running instance
                //    app.Quit();
                    
                //}
                //outlookApp.Quit();
               // process.Close();
                process.Kill();
            }
            catch (Exception objEx)
            {
                string error = objEx.ToString();
            }

        }
    }
}
