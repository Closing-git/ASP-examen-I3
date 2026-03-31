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
        private readonly UserSessionManager _userSession;

        public AuthController(IUserRepository<BLL.Entities.User> bllService, UserSessionManager userSession)
        {
            _bllService = bllService;
            _userSession = userSession;

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
                Guid employeeId = _bllService.CheckPassword(form.Email, form.Password);
                _userSession.EmployeeId = employeeId;


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
                _userSession.Clear();
                return RedirectToAction(nameof(Login));
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
