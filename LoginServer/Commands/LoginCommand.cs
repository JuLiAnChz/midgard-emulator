using Core.Network;

namespace LoginServer.Commands
{
    public abstract class LoginCommand : ICommand
    {
        protected byte[] _packedData = new byte[0];
        protected UInt16 _packetHeader;
        public abstract void Execute(byte[] packedData, string ipAddress);

        public bool validatePackedLength()
        {
            if((_packetHeader == 0x0064 && _packedData.Length < 55) ||
                (_packetHeader == 0x0277 && _packedData.Length < 84) ||
                (_packetHeader == 0x02b0 && _packedData.Length < 85) ||
                (_packetHeader == 0x01dd && _packedData.Length < 47) ||
                (_packetHeader == 0x01fa && _packedData.Length < 48) ||
                (_packetHeader == 0x027c && _packedData.Length < 60) ||
                (_packetHeader == 0x0825 && (_packedData.Length < 4 || _packedData.Length < _packedData[2])))
                return false;
            return true;
        }
    }
}
