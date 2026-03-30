using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface IEmployeeRepository<TEmployee> : ICRUDRepository<TEmployee, Guid>
    {
        public bool CheckIsProjectManager(Guid employeeId);
        public bool CheckIsOnProject(Guid employeeId, Guid projectId);
        public IEnumerable<TEmployee> GetByProjectId(Guid projectId);
        public TEmployee GetByUserId(Guid userId);
        public IEnumerable<TEmployee> GetAvailableEmployees();
    }
}
