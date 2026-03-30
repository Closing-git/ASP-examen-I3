using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectManager.Common.Repositories;


namespace ProjectManager.ASPMVC.Handlers.Filters
{
    public class ProjectManagerFilter : IAuthorizationFilter
    {
        private readonly UserSessionManager _userSession;
        private readonly IEmployeeRepository<BLL.Entities.Employee> _employeeService;
        public ProjectManagerFilter(
            UserSessionManager userSession, IEmployeeRepository<BLL.Entities.Employee> employeeService)
        {
            _employeeService = employeeService;
            this._userSession = userSession;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Guid? employeeId = _userSession.EmployeeId;
            if (!employeeId.HasValue)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
                return;
            }
            if (_employeeService.CheckIsProjectManager(employeeId.Value) == false)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }

        }
    }
}
