using ProjectManager.BLL.Entities;
using ProjectManager.BLL.Mappers;
using ProjectManager.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Services
{
    public class ProjectService : IProjectRepository<Project>
    {
        private readonly IProjectRepository<DAL.Entities.Project> _dalService;
        public ProjectService(IProjectRepository<DAL.Entities.Project> dalService)
        {
            _dalService = dalService;
        }

        public Guid Create(Project entity)
        {
            return _dalService.Create(entity.ToDAL());
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> Get()
        {
            throw new NotImplementedException();
        }

        public Project Get(Guid id)
        {
            return _dalService.Get(id).ToBLL();
        }

        public Project GetByEmployeeId(Guid employeeId)
        {
            return _dalService.GetByEmployeeId(employeeId).ToBLL();
        }

        public Project GetByManagerId(Guid managerId)
        {
            return _dalService.GetByManagerId(managerId).ToBLL();
        }

        public void Update(Guid id, Project entity)
        {
            _dalService.Update(id, entity.ToDAL());
        }
    }
}
