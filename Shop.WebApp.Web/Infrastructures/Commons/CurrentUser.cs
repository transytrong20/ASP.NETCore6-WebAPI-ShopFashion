using Newtonsoft.Json;
using Shop.Webapp.Shared.ApiModels;
using Shop.Webapp.Shared.ConstsDatas;

namespace Shop.WebApp.Web.Infrastructures.Commons
{
    public class CurrentUser : ICurrentUser
    {
        private readonly Guid? _id;
        private readonly string _surname;
        private readonly string _name;
        private readonly string _username;
        private readonly string _email;
        private readonly string _phone;
        private readonly string[] _roles;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.Id) != null)
                _id = Guid.Parse(httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.Id)?.Value);
            if (httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.SurName) != null)
                _surname = httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.SurName)?.Value;
            if (httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.Name) != null)
                _name = httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.Name)?.Value;
            if (httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.Username) != null)
                _username = httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.Username)?.Value;
            if (httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.Email) != null)
                _email = httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.Email)?.Value;
            if (httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.PhoneNumber) != null)
                _phone = httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.PhoneNumber)?.Value;
            if (httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.Role) != null)
                _roles = JsonConvert.DeserializeObject<string[]>(httpContextAccessor.HttpContext?.User?.FindFirst(CustomeClaimTypes.Role)?.Value);
        }

        public bool IsAuthenticated => _id.HasValue;

        public Guid? Id => _id;

        public string SurName => _surname;

        public string Name => _name;

        public string Username => _username;

        public string Email => _email;

        public string PhoneNumber => _phone;

        public string[] Roles => _roles;

        public bool IsInRole(string roleName)
        {
            return Roles.Any(x => x == roleName);
        }
    }
}
