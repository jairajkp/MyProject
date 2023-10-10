using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckinReviewReport
{
    class CodeReviewComment
    {
        private string _Reviewer;
        private string _Author;
        private string _ReviewComment;
        private string _ReviewWorkItem;
        private string _AuthorComment;
        private string _PublishDate;
        private string _ItemName;
        private string _ChangesetIds;
        private string _ChangeSetComments;
        private string _ChangeSetCommitter;
        private string _WorkItemState;



        public string Author { get => _Author; set => _Author = value; }
        public string AuthorComment { get => _AuthorComment; set => _AuthorComment = value; }
        public string PublishDate { get => _PublishDate; set => _PublishDate = value; }
        public string ItemName { get => _ItemName; set => _ItemName = value; }
        public string ChangesetIds { get => _ChangesetIds; set => _ChangesetIds = value; }
        public string ChangeSetComments { get => _ChangeSetComments; set => _ChangeSetComments = value; }
        public string ChangeSetCommitter { get => _ChangeSetCommitter; set => _ChangeSetCommitter = value; }
        public string ReviewWorkItem { get => _ReviewWorkItem; set => _ReviewWorkItem = value; }
        public string WorkItemState { get => _WorkItemState; set => _WorkItemState = value; }
        public string Reviewer { get => _Reviewer; set => _Reviewer = value; }
        public string ReviewComments { get => _ReviewComment; set => _ReviewComment = value; }
       

    }
}
