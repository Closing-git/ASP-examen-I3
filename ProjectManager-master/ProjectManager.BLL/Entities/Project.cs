using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class Project
    {
        public Guid ProjectId { get; private set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                if (value.Length > 256) throw new FormatException();
                _name = value;
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                _description = value;
            }
        }
        public DateTime CreationDate { get; private set; }
        public Guid ProjectManagerId { get; private set; }

        public Project(string name, string description Guid projectManagerId)
        {
            ProjectId = Guid.NewGuid();
            Name = name;
            Description = description;
            CreationDate = DateTime.Now;
            ProjectManagerId = projectManagerId;
        }

        public Project(Guid projectId, string name, string description, DateTime creationDate, Guid projectManagerId)
        {
            ProjectId = projectId;
            Name = name;
            Description = description;
            CreationDate = creationDate;
            ProjectManagerId = projectManagerId;
        }
    }
}
