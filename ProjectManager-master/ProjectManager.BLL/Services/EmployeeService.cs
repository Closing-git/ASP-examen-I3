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
    public class EmployeeService:IEmployeeRepository<Employee>
    {
        private readonly IEmployeeRepository<DAL.Entities.Employee> _dalService;

        public EmployeeService(IEmployeeRepository<DAL.Entities.Employee> dalService)
        {
            _dalService = dalService;
        }

        public bool CheckIsOnProject(Guid employeeId, Guid projectId)
        {
            return _dalService.CheckIsOnProject(employeeId, projectId);
        }

        public bool CheckIsProjectManager(Guid employeeId)
        {
            return _dalService.CheckIsProjectManager(employeeId);
        }

        public Guid Create(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> Get()
        {
            throw new NotImplementedException();
        }

        public Employee Get(Guid id)
        {
            return _dalService.Get(id).ToBLL();
        }

        public IEnumerable<Employee> GetAvailableEmployees()
        {
            return _dalService.GetAvailableEmployees().Select(employee => employee.ToBLL());
        }

        public IEnumerable<Employee> GetByProjectId(Guid projectId)
        {
            return _dalService.GetByProjectId(projectId).Select(employee => employee.ToBLL());
        }

        public Employee GetByUserId(Guid userId)
        {
            
            return _dalService.GetByUserId(userId).ToBLL();
        }

        public void Update(Guid id, Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
