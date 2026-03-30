using Microsoft.AspNetCore.Mvc;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.ASPMVC.Handlers.Filters;
using ProjectManager.ASPMVC.Models.Auth;
using ProjectManager.BLL.Services;
using ProjectManager.Common.Repositories;

namespace ProjectManager.ASPMVC.Controllers
{
    public class AuthController : Controller
    {

        private readonly IUserRepository<BLL.Entities.User> _bllService;
        private readonly IEmployeeRepository<BLL.Entities.Employee> _employeeService;
        private readonly UserSessionManager _userSession;

        public AuthController(IUserRepository<BLL.Entities.User> bllService, UserSessionManager userSession, IEmployeeRepository<BLL.Entities.Employee> employeeService)
        {
            _bllService = bllService;
            _userSession = userSession;
            _employeeService = employeeService;
        }


        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        [TypeFilter<AnonymousFilter>]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [TypeFilter<AnonymousFilter>]
        public IActionResult Login(LoginForm form)
        {
            try
            {
                if (!ModelState.IsValid) throw new InvalidOperationException("Le formulaire n'est pas valide.");
                Guid id = _bllService.CheckPassword(form.Email, form.Password);
                _userSession.UserId = id;
                BLL.Entities.Employee employee = _employeeService.GetByUserId(id);
                _userSession.EmployeeId = employee.EmployeeId;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View();
            }
        }

        [TypeFilter<RequiredAuthenticationFilter>]
        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        [TypeFilter<RequiredAuthenticationFilter>]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout(IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid) throw new InvalidOperationException();
                _userSession.UserId = null;
                _userSession.EmployeeId = null;
                return RedirectToAction(nameof(Login));
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
