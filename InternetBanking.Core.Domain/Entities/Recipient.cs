﻿using InternetBanking.Core.Domain.Common;
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
        public int UserId { get; set; }
        public int RecipientCode { get; set; }

    }
}
