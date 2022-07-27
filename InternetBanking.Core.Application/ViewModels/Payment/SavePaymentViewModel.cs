using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Payment
{
    public class SavePaymentViewModel
    {
        public double AmountToPay { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Text)]
        public string PaymentAccount { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Text)]
        public string PaymentDestinationAccount { get; set; }

        public string Owner { get; set; }
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
