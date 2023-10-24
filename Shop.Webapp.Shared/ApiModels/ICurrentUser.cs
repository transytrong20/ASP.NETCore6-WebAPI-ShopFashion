namespace Shop.Webapp.Shared.ApiModels
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        Guid? Id { get; }
        string SurName { get; }
        string Name { get; }
        string Username { get; }
        string Email { get; }
        string PhoneNumber { get; }
        string[] Roles { get; }

        bool IsInRole(string roleName);
    }
}
