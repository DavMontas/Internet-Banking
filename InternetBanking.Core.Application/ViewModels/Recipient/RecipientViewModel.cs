using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Recipient
{
    public class RecipientViewModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public UserViewModel User { get; set; }

        public string RecipientCode { get; set; }
        public  ProductViewModel Recipient { get; set; }
        public string OwnerAccount { get; set; }

    }
}
