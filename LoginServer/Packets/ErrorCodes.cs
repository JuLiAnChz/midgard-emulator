using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Packets
{
    public static class ErrorCodes
    {
        public const int USERNAME_PASSWORD_INCORRECT = 1;
        public const int ID_IS_EXPIRED = 2;
        public const int REJECTED_FROM_SERVER = 3;
        public const int ACCOUNT_ID_BLOCKED_BY_GM = 4;
        public const int GAME_EXE_NOT_LATEST = 5;
        public const int PROHIBITED_LOG_IN_BY = 6;
        public const int SERVER_IS_FULL = 7;
        public const int ACCOUNT_CANT_CONNECTED_WITH_SERVER = 8;
    }
}
