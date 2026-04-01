using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Post
{
    public class ListItemViewModel
    {

        public Guid PostId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime SendDate { get; set; }
        public Guid ProjectId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string ProjectName { get; set; }
    }
}
