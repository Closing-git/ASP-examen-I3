using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Project
{
    public class CreateForm
    {
        [DisplayName("Nom : ")]
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [MaxLength(256, ErrorMessage = "Le nom ne peut dépasser 255 caractères.")]
        public string Name { get; set; }
        [DisplayName("Description : ")]
        [Required(ErrorMessage = "La description est obligatoire.")]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public Guid ProjectManagerId { get; set; }

    }
}
