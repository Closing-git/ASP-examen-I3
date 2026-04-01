using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Post
{
    public class ListItemViewModel
    {

        public Guid PostId { get; set; }
        [DisplayName("Sujet")]
        public string Subject { get; set; }
        [DisplayName("Message")]
        public string Content { get; set; }
        public Guid EmployeeId { get; set; }
        [DisplayName("Date d'envoi")]
        public DateTime SendDate { get; set; }
        public Guid ProjectId { get; set; }
        [DisplayName("Auteur")]
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        [DisplayName("Nom du projet")]
        public string ProjectName { get; set; }
    }
}
