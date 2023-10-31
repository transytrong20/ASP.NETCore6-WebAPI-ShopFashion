using Shop.Webapp.Shared.ApiModels.Requests;

namespace Shop.Webapp.Application.RequestObjects
{
    public class CreateUserModel
    {
        public string? SurName { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Guid RoleId { get; set; } = new Guid("9fdf3383-125f-4b87-a8d9-9e235a4e3652");
    }

    public class UpdateUserModel
    {
        public string? SurName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }

    public class UserPagingFilter : GenericPagingFilter
    {
        public Guid? RoleId { get; set; }
    }
}
