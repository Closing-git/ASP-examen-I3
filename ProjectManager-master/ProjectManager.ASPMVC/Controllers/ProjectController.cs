using Microsoft.AspNetCore.Mvc;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.ASPMVC.Handlers.Filters;
using ProjectManager.ASPMVC.Mappers;
using ProjectManager.ASPMVC.Models.Project;
using ProjectManager.BLL.Entities;
using ProjectManager.Common.Repositories;

namespace ProjectManager.ASPMVC.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository<BLL.Entities.Project> _bllService;
        private readonly IEmployeeRepository<BLL.Entities.Employee> _employeeService;
        private readonly UserSessionManager _userSession;

        public ProjectController(IProjectRepository<BLL.Entities.Project> bllService, IEmployeeRepository<BLL.Entities.Employee> employeeService, UserSessionManager userSession)
        {
            _bllService = bllService;
            _employeeService = employeeService;
            _userSession = userSession;
        }

        [TypeFilter<RequiredAuthenticationFilter>]  
        public IActionResult Index()
        {

            IEnumerable<ListItemViewModel> model;
            model = _bllService.GetByEmployeeId(_userSession.EmployeeId.Value).Select(bll => bll.ToListItem());

            return View(model);
        }
    }
}
