using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Payment
{
    public class PaymentViewModel
    {
        public double AmountToPay { get; set; }
        public string PaymentAccount { get; set; }
        public string PaymentDestinationAccount { get; set; }
    }
}
