using InternetBanking.Core.Domain.Common;


namespace InternetBanking.Infrastructure.Identity.Entities
{
    public class UserProduct : AuditableBE
    {
        public int IdTypeAccount { get; set; }
        public TypeAccount NameAccount { get; set; }
        public double Amount { get; set; }
    }
}
