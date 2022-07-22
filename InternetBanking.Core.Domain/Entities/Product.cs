using InternetBanking.Core.Domain.Common;
using InternetBanking.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Domain.Entities
{
    public class Product : AuditableBE
    {
        public int IdAccount { get; set; }
        public TypeAccount  TypeAccount { get; set; }
        public double Amount { get; set; }
        public string IdClient { get; set; }
        public int Code { get; set; }
        public double Paid { get; set; }

    }
}
