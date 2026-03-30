using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class TakePart
    {

        public Guid EmployeeId { get; private set; }
        public Guid ProjectId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public TakePart(Guid employeeId, Guid projectId, DateTime startDate)
        {
            EmployeeId = employeeId;
            ProjectId = projectId;
            StartDate = DateTime.Now;
        }

        public TakePart(Guid employeeId, Guid projectId, DateTime startDate, DateTime? endDate) : this(employeeId, projectId, startDate)
        {
            EndDate = endDate;
        }

        public void EndParticipation()
        {
            EndDate = DateTime.Now;
        }
    }
}
