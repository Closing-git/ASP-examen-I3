using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface ITakePartRepository<TTakePart> : ICRUDRepository<TTakePart, Guid>
    {
        public void setEnd(Guid employeeId, Guid projectId, DateTime endTime);
    }
}
