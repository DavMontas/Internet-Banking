using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Recipient
{
    public class RecipientSaveViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public string RecipientCode { get; set; }

    }
}
