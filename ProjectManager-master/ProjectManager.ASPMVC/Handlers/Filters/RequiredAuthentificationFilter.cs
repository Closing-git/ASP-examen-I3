using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace ProjectManager.ASPMVC.Handlers.Filters
{

    public class RequiredAuthenticationFilter : IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                ISession session = context.HttpContext.Session;

                Guid? userId = JsonSerializer.Deserialize<Guid?>(session.GetString("UserId") ?? "null");

                if (userId is null)
                {
                    context.Result = new RedirectToActionResult("Login", "Auth", null);
                }
            }
        }
    }
