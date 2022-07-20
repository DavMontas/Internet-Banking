using InternetBanking.Core.Domain.Common;


namespace InternetBanking.Infrastructure.Identity.Entities
{
    public class TypeAccount : AuditableBE
    {
        public string NameAccount { get; set; }
    }
}
