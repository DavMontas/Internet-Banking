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
        public UserViewModel User { get; set; }



        public int RecipientCode { get; set; }
        public  UserViewModel Recipient { get; set; }


    }
}
