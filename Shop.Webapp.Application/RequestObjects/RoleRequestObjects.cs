using Shop.Webapp.Shared.ApiModels.Requests;

namespace Shop.Webapp.Application.RequestObjects
{
    public class AddRoleModel
    {
        public string? Name { get; set; }
        public bool? IsDefault { get; set; }
        public bool? Static { get; set; }
        public string? Description { get; set; }
    }

    public class RolePagingFilter : GenericPagingFilter
    {
        public string? RoleId { get; set; }
    }
}
