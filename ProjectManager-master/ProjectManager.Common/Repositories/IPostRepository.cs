using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface IPostRepository<TPost>: ICRUDRepository<TPost, Guid>
    {
        public IEnumerable<TPost> GetByProjectId(Guid projectId);
        public IEnumerable<TPost> GetWorkingOnProject(Guid projectId, Guid employeeId);
    }
}
