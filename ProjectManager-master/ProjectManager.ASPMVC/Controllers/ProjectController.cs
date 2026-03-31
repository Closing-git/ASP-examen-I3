using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
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
        #region Constructor
        private readonly IProjectRepository<BLL.Entities.Project> _bllService;
        private readonly IEmployeeRepository<BLL.Entities.Employee> _employeeService;
        private readonly UserSessionManager _userSession;

        public ProjectController(IProjectRepository<BLL.Entities.Project> bllService, UserSessionManager userSession, IEmployeeRepository<Employee> employeeService)
        {
            _bllService = bllService;
            _userSession = userSession;
            _employeeService = employeeService;
        }
        #endregion


        #region Index / List
        [TypeFilter<RequiredAuthenticationFilter>]
        public IActionResult Index()
        {
            Guid employeeId = _userSession.EmployeeId.Value;
            bool isProjectManager = _employeeService.CheckIsProjectManager(employeeId);
            IEnumerable<ListItemViewModel> model;

            if (isProjectManager)
            {
                model = _bllService.GetByManagerId(employeeId).Select(bll => bll.ToListItem());
            }
            else
            {
                model = _bllService.GetByEmployeeId(employeeId).Select(bll => bll.ToListItem());
            }

            return View(model);
        }
        #endregion


        #region Create
        [TypeFilter<ProjectManagerFilter>]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [TypeFilter<ProjectManagerFilter>]
        public ActionResult Create(CreateForm form)
        {
            try
            {
                form.ProjectManagerId = _userSession.EmployeeId.Value;
                if (!ModelState.IsValid) throw new InvalidOperationException("Le formulaire n'est pas valide.");
                Guid projectId = _bllService.Create(form.ToBLL());
                return RedirectToAction(nameof(Details), "Project", new { id = projectId });
            }
            catch
            {
                return View(form);
            }
        }
        #endregion

        #region Details

        [TypeFilter<RequiredAuthenticationFilter>]
        public IActionResult Details(Guid id)
        {
            DetailsViewModel model = _bllService.Get(id).ToDetails();
            return View(model);
        }

        #endregion


    }
}

