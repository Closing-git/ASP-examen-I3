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


        public Guid? EmployeeId
        {
            get
            {
                return JsonSerializer.Deserialize<Guid?>(_session.GetString(nameof(EmployeeId)) ?? "null");
            }
            set
            {
                if (value is null) _session.Remove(nameof(EmployeeId));
                else _session.SetString(nameof(EmployeeId), JsonSerializer.Serialize(value));
            }
        }
        public void Clear()
        {

            EmployeeId = null;
        }
    }
}

