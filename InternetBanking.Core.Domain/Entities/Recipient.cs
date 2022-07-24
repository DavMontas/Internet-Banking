using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Domain.Entities
{
    public class Recipient
    {
        public int Id { get; set; }
        public int RecipientCode { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
