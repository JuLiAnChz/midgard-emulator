using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class StringManager
    {
        public static string ByteToString(byte[] source, int n)
        {
            if(n > 0)
            {
                byte[] destination = new byte[source.Length];
                int destIndex = 0;
                int sourceIndex = 0;

                destination[--n] = 0;

                for(; n > 0;--n)
                {
                    byte currentByte = source[sourceIndex++];
                    destination[destIndex++] = currentByte;

                    if(currentByte == 0)
                    {
                        while(--n > 0)
                        {
                            destination[destIndex++] = 0;
                        }
                        break;
                    }
                }

                return Encoding.UTF8.GetString(destination).Trim('\0');
            }

            return string.Empty;
        }
    }
}
