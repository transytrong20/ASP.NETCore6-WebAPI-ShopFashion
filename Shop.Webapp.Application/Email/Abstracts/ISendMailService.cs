using Shop.Webapp.Application.Email.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Webapp.Application.Email.Abstracts
{
    public interface ISendMailService
    {
        public void SendEmailAsync(EmailModel email);
    }
}
