using InternetBanking.Core.Domain.Common;
using InternetBanking.Core.Domain.Entities;
using System.Collections.Generic;

namespace InternetBanking.Infrastructure.Identity.Entities
{
    public class TypeAccount : AuditableBE
    {
        public string NameAccount { get; set; }
        public ICollection<Product> MyProperty { get; set; }
    }
}
