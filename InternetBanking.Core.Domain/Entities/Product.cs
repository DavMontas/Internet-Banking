using InternetBanking.Core.Domain.Common;
using InternetBanking.Infrastructure.Identity.Entities;

namespace InternetBanking.Core.Domain.Entities
{
    public class Product : AuditableBE
    {
        public double Charge { get; set; }
        public string ClientId { get; set; }
        public int Code { get; set; }
        public double Discharge { get; set; }

        public int TypeAccountId { get; set; }
        public TypeAccount TypeAccount { get; set; }
    }
}
