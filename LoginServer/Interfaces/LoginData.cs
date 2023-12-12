using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Interfaces
{
    public class LoginData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Int32 ClientType { get; set; }
    }
}
