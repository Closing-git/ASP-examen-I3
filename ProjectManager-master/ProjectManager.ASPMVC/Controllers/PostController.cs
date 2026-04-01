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
    public class PostController : Controller
    {

        #region Constructor

        private readonly IPostRepository<BLL.Entities.Post> _bllService;
        private readonly IProjectRepository<BLL.Entities.Project> _projectService;
        private readonly IEmployeeRepository<BLL.Entities.Employee> _employeeService;
        private readonly IUserRepository<BLL.Entities.User> _userService;
        private readonly UserSessionManager _userSession;

        public PostController(IPostRepository<BLL.Entities.Post> bllService,
            UserSessionManager userSession,
            IProjectRepository<BLL.Entities.Project> projectRepository,
            IEmployeeRepository<BLL.Entities.Employee> employeeRepository,
            IUserRepository<User> userService)
        {
            _bllService = bllService;
            _userSession = userSession;
            _projectService = projectRepository;
            _employeeService = employeeRepository;
            _userService = userService;
        }

        #endregion



        #region Index / List
        [TypeFilter<RequiredAuthenticationFilter>]
        public IActionResult Index()
        {
            Guid employeeId = _userSession.EmployeeId.Value;
            IEnumerable<Post> posts;


            IEnumerable<Project> projects = _projectService.GetByEmployeeId(employeeId);

            if (_employeeService.CheckIsProjectManager(employeeId))
            {
                IEnumerable<Project> managedProjects = _projectService.GetByManagerId(employeeId);
                posts = managedProjects
            .SelectMany(project => _bllService.GetByProjectId(project.ProjectId))
            .OrderByDescending(p => p.SendDate);
            }
            else
            {
                posts = projects
                    .SelectMany(project => _bllService.GetWorkingOnProject(project.ProjectId, employeeId))
                    .OrderByDescending(post => post.SendDate);

            }

            var model = new List<Models.Post.ListItemViewModel>();


            foreach (var post in posts)
            {
                var author = _employeeService.Get(post.EmployeeId);
                string authorName = $"{author.FirstName} {author.LastName}";

                var authorUser = _userService.GetFromEmployeeId(post.EmployeeId);
                var authorEmail = authorUser.Email;

                var project = _projectService.Get(post.ProjectId);
                string projectName = project.Name;


                model.Add(
                    new Models.Post.ListItemViewModel
                    {
                        PostId = post.PostId,
                        Subject = post.Subject,
                        Content = post.Content,
                        EmployeeId = post.EmployeeId,
                        SendDate = post.SendDate,
                        ProjectId = post.ProjectId,
                        AuthorName = authorName,
                        AuthorEmail = authorEmail,
                        ProjectName = projectName
                    });
            }

            return View(model);
        }
        #endregion


        #region Create
        [TypeFilter<RequiredAuthenticationFilter>]
        public ActionResult Create(Guid id)
        {
            Models.Post.CreateForm createForm = new Models.Post.CreateForm
            {
                ProjectId = id,
                EmployeeId = _userSession.EmployeeId.Value
            };

            return View(createForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [TypeFilter<RequiredAuthenticationFilter>]

        public ActionResult Create(Models.Post.CreateForm form)
        {
            try
            {
                if (!ModelState.IsValid) throw new InvalidOperationException("Le formulaire n'est pas valide.");
                Guid postId = _bllService.Create(form.ToBLL());

                return RedirectToAction(nameof(Index), "Post");
            }
            catch
            {
                return View(form);
            }
        }
        #endregion

    }
}
