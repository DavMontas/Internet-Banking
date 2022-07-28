using InternetBanking.Core.Application.ViewModels.User;
using InternetBanking.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public double Charge { get; set; }
        public string ClientId { get; set; }
        public string AccountNumber { get; set; }
        public double Discharge { get; set; }
        public bool IsPrincipal { get; set; } = false;
        public int TypeAccountId { get; set; }
        public TypeAccount TypeAccount { get; set; }
        public string Owner { get; set; }

        public UserViewModel Client { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
