using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Project
{
    public class ListItemViewModel
    {
        [ScaffoldColumn (false)]
        public Guid ProjectId { get; set; }
        [DisplayName("Nom : " )]
        public string Name { get; set; }
        [DisplayName("Description : ")]
        public string Description { get; set; }
        [DisplayName("Nombre de membres ")]
        public int nbMembers { get; set; }
    }
}
