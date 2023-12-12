using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config.Interfaces
{
    public class LoginServerConfig
    {
        public int port {  get; set; }
        public string Ip {  get; set; }
        public bool EnabledUserCount { get; set; }
        public int UserCountLow {  get; set; }
        public int UserCountMedium {  get; set; }
        public int UserCountHigh {  get; set; }
    }
}
