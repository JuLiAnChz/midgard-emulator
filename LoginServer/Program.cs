using System;

namespace LoginServer
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            Core.Init();

            while (true)
            {
                Thread.Sleep(0);
            }
        }
    }
}