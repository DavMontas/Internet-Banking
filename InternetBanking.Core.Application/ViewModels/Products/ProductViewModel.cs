using InternetBanking.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Products
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public double Charge { get; set; }
        public string ClientId { get; set; }
        public int Code { get; set; }
        public double Discharge { get; set; }

        public int TypeAccountId { get; set; }
        public TypeAccount TypeAccount { get; set; }
    }
}
