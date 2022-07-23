using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Dtos.Account
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string IdCard { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public int TypeUser { get; set; }
        public bool IsVerified { get; set; } = false;
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
