using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Packets
{
    public static class LoginPackets
    {
        public const byte USERNAME_AND_PASSWORD = 0x0064;
        public const int USERNAME_AND_PASSWORD_LENGTH = 55;
    }
}
