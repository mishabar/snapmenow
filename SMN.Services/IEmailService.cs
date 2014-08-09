using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN.Services
{
    public interface IEmailService
    {
        void Send(string to, string subject, string body);
    }
}
