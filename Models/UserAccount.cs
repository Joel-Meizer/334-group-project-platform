namespace _334_group_project_web_api.Models
{
    public enum UserAccountType
    {
        Administrator,
        ChildUser,
        AdolescentUser,
        AdultUser
    }
    public class UserAccount
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? givenName { get; set; }
        public string? surname { get; set; }
        public string? displayName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? EmailConfirmed { get; set; }
        public UserAccountType? type {  get; set; }
        public string? PhoneNumber { get; set; }
        public string? street { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? postalCode { get; set; }
        public string? country { get; set; }

    }
}
