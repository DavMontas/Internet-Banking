using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Helpers
{
    public class AccountNumberGenerator
    {
        public string NumberGenerator()
        {
            Random randomNumber = new Random();
            int number = randomNumber.Next(1, 1000000000);
            string generatedCode = number.ToString("000000000");
            return generatedCode;
        }

    }
}
