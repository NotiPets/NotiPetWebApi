using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notipet.Services.Mail
{
    internal interface IMailService
    {
        public Task SendMailAsync(string content);
    }
}
