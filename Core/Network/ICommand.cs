using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Network
{
    public interface ICommand
    {
        void Execute(byte[] packetData, string ipAddress);
    }
}
