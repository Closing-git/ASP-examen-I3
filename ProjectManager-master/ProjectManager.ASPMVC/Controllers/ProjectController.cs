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
        private readonly IPostRepository<BLL.Entities.Post> _postService;
        private readonly IUserRepository<BLL.Entities.User> _userService;
        private readonly UserSessionManager _userSession;

        public ProjectController(IProjectRepository<BLL.Entities.Project> bllService,
            UserSessionManager userSession,
            IEmployeeRepository<Employee> employeeService,
            IPostRepository<Post> postService,
            IUserRepository<User> userService)
        {
            _bllService = bllService;
            _userSession = userSession;
            _employeeService = employeeService;
            _postService = postService;
            _userService = userService;
        }
        #endregion


        #region Index / List
        [TypeFilter<RequiredAuthenticationFilter>]
        public IActionResult Index()
        {
            Guid employeeId = _userSession.EmployeeId.Value;
            bool isProjectManager = _employeeService.CheckIsProjectManager(employeeId);
            IEnumerable<Project> result;
            List<ListItemViewModel> model = new List<ListItemViewModel>();


            if (isProjectManager)
            {
                result = _bllService.GetByManagerId(employeeId);
            }
            else
            {
                result = _bllService.GetByEmployeeId(employeeId);
            }

            foreach (var project in result)
            {
                var membersCount = _employeeService.GetByProjectId(project.ProjectId).Count();

                model.Add(new ListItemViewModel
                {
                    ProjectId = project.ProjectId,
                    Name = project.Name,
                    Description = project.Description,
                    nbMembers = membersCount
                });
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
            IEnumerable<Models.Post.ListItemViewModel> posts = _postService.GetByProjectId(id)
                .OrderByDescending(post => post.SendDate)
                .Select(post => post.ToListItem());

            List<Models.Post.ListItemViewModel> postsWithData = new List<Models.Post.ListItemViewModel>();

            foreach (Models.Post.ListItemViewModel post in posts)
            {
                Employee author = _employeeService.Get(post.EmployeeId);
                string authorName = $"{author.FirstName} {author.LastName}";

                User authorUser = _userService.GetFromEmployeeId(post.EmployeeId);
                string authorEmail = authorUser.Email;

                postsWithData.Add(new Models.Post.ListItemViewModel
                {
                    PostId = post.PostId,
                    Subject = post.Subject,
                    Content = post.Content,
                    EmployeeId = post.EmployeeId,
                    SendDate = post.SendDate,
                    AuthorName = authorName,
                    AuthorEmail = authorEmail,
                });
            }

            model.ProjectManagerName = $"{_employeeService.Get(model.ManagerId).FirstName} {_employeeService.Get(model.ManagerId).LastName}";
            model.Team = _employeeService.GetByProjectId(id).Select(e => $"{e.FirstName} {e.LastName}");
            model.Posts = postsWithData;
            return View(model);
        }

        #endregion


    }
}

