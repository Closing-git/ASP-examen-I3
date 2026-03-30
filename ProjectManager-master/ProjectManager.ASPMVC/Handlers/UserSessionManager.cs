using System.Text.Json;

namespace ProjectManager.ASPMVC.Handlers
{
    public class UserSessionManager
    {
        private readonly ISession _session;
        public UserSessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public Guid? UserId
        {
            get
            {
                return JsonSerializer.Deserialize<Guid?>(_session.GetString(nameof(UserId)) ?? "null");
            }
            set
            {
                if (value is null) _session.Remove(nameof(UserId));
                else _session.SetString(nameof(UserId), JsonSerializer.Serialize(value));
            }
        }
    }
}

