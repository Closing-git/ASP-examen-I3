using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using ProjectManager.Common.Repositories;


namespace ProjectManager.ASPMVC.Handlers.Filters
{
    public class ProjectManagerFilter : IAuthorizationFilter
    {
        
        private readonly IEmployeeRepository<BLL.Entities.Employee> _employeeService;
        public ProjectManagerFilter(IEmployeeRepository<BLL.Entities.Employee> employeeService)
        {
            _employeeService = employeeService;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ISession session = context.HttpContext.Session;
            Guid? employeeId = JsonSerializer.Deserialize<Guid?>(session.GetString("EmployeeId") ?? "null");
            if (employeeId is null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }
            if (_employeeService.CheckIsProjectManager(employeeId.Value) == false)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
                
            }

        }
    }
}
