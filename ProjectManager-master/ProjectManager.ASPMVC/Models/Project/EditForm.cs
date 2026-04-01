using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Project
{
    public class EditForm
    {
        [ScaffoldColumn(false)]
        public Guid ProjectId { get; set; }

        
        [DisplayName("Description : ")]
        [Required(ErrorMessage = "La description est obligatoire.")]
        public string Description { get; set; }

        public string ProjectName { get; set; }
        public Guid  ManagerId { get; set; }
    }
}
