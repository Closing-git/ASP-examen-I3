using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ASPMVC.Models.Project
{
    public class DetailsViewModel
    {
        [ScaffoldColumn(false)]
        public Guid ProjectId { get; set; }
        [ScaffoldColumn(false)]
        public Guid ManagerId { get; set; }

        [DisplayName("Nom du projet :")]
        public string Name { get; set; }
        [DisplayName("Description:")]
        public string Description { get; set; }
        [DisplayName("Date de début:")]
        public string CreationDate { get; set; }
        [DisplayName("Chef de projet:")]
        public string ProjectManagerName { get; set; }
        [DisplayName("Equipe:")]
        public IEnumerable<String> Team { get; set; }

    }
}
