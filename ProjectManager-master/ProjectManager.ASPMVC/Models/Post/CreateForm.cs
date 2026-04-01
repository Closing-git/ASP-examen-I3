using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Post
{
    public class CreateForm
    {
        [DisplayName("Sujet : ")]
        [Required(ErrorMessage = "Le sujet est obligatoire.")]
        [MaxLength(256, ErrorMessage = "Le sujet ne peut dépasser 256 caractères.")]
        public string Subject { get; set; }
        [DisplayName("Contenu : ")]
        [Required(ErrorMessage = "Le contenu du message est obligatoire.")]
        public string Content { get; set; }

        [ScaffoldColumn(false)]
        public Guid EmployeeId { get; set; }
        [ScaffoldColumn(false)]
        public Guid ProjectId { get; set; }
      
    }
}
