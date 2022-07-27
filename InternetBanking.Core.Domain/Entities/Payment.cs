using InternetBanking.Core.Domain.Common;
using InternetBanking.Infrastructure.Identity.Entities;

namespace InternetBanking.Core.Domain.Entities
{
    public class Payment : AuditableBE
    {
        public double AmountToPay { get; set; }
        public string PaymentAccount { get; set; }
        public string PaymentDestinationAccount { get; set; }
    }
}
