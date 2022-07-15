using InternetBanking.Core.Domain.Common;


namespace InternetBanking.Infrastructure.Identity.Entities
{
    public class User : AuditableBE
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
