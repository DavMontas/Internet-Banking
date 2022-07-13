using InternetBanking.Core.Application.Dtos;
using InternetBanking.Core.Application.Dtos.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest req);
    }
}
