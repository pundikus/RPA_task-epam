using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA_Onliner_Bot.Service.Abstract
{
    public interface IEmailService
    {
        bool SendMessage(bool result, string path, string address);
    }
}
