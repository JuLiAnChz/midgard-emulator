using System;

namespace CharServer
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