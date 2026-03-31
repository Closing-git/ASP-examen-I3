using Microsoft.AspNetCore.Mvc;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.ASPMVC.Handlers.Filters;
using ProjectManager.ASPMVC.Models.Project;
using ProjectManager.BLL.Entities;
using ProjectManager.Common.Repositories;

namespace ProjectManager.ASPMVC.Controllers
{
    public class PostController : Controller
    {

        #region Constructor

        private readonly IPostRepository<BLL.Entities.Project> _bllService;
        private readonly IProjectRepository<BLL.Entities.Project> _projectService;
        private readonly UserSessionManager _userSession;

        public PostController(IPostRepository<BLL.Entities.Project> bllService, UserSessionManager userSession, IProjectRepository<BLL.Entities.Project> projectRepository)
        {
            _bllService = bllService;
            _userSession = userSession;
            _projectService = projectRepository;
        }

        #endregion



        #region Index / List
        [TypeFilter<RequiredAuthenticationFilter>]
        public IActionResult Index()
        {
            Guid employeeId = _userSession.EmployeeId.Value;

            var model = _projectService
                .GetByEmployeeId(employeeId)
                .SelectMany(project => _bllService.GetWorkingOnProject(project.ProjectId, employeeId));

            return View(model);
        }
        #endregion

    }
}
