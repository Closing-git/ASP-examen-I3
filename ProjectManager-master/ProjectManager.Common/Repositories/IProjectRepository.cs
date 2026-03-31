using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface IProjectRepository<TProject> : ICRUDRepository<TProject, Guid>
    {
        public IEnumerable<TProject> GetByEmployeeId(Guid employeeId);
        public IEnumerable<TProject> GetByManagerId(Guid managerId);
    }
}
