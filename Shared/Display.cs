using ColoredConsole;
using System;

namespace Shared
{
    public class Display
    {
        public static void Header()
        {
            ColorConsole.WriteLine("'##::::'##:'####:'########:::'######::::::'###::::'########::'########::::".Green());
            ColorConsole.WriteLine("###::'###:. ##:: ##.... ##:'##... ##::::'## ##::: ##.... ##: ##.... ##::::".Green());
            ColorConsole.WriteLine("####'####:: ##:: ##:::: ##: ##:::..::::'##:. ##:: ##:::: ##: ##:::: ##::::".Green());
            ColorConsole.WriteLine("## ### ##:: ##:: ##:::: ##: ##::'####:'##:::. ##: ########:: ##:::: ##::::".Green());
            ColorConsole.WriteLine("##. #: ##:: ##:: ##:::: ##: ##::: ##:: #########: ##.. ##::: ##:::: ##::::".Green());
            ColorConsole.WriteLine("##:.:: ##:: ##:: ##:::: ##: ##::: ##:: ##.... ##: ##::. ##:: ##:::: ##::::".Green());
            ColorConsole.WriteLine("##:::: ##:'####: ########::. ######::: ##:::: ##: ##:::. ##: ########:::::".Green());
            ColorConsole.WriteLine("..:::::..::....::........::::......::::..:::::..::..:::::..::........:::::".Green());
        }

        public static void BreakLine()
        {
            ColorConsole.WriteLine("==========================================================================");
        }

        public static void Error(string message)
        {
            ColorConsole.WriteLine("[","Error".Red(),"]: ", message);
        }
        
        public static void Info(string message)
        {
            ColorConsole.WriteLine("[Info]: ", message);
        }

        public static void Success(string message)
        {
            ColorConsole.WriteLine("[", "Success".Green(), "]: ", message);
        }

        public static void Warn(string message)
        {
            ColorConsole.WriteLine("[", "Warn".Yellow(), "]: ", message);
        }

        public static void Debug(string message)
        {
            ColorConsole.WriteLine("[", "DEBUG".DarkCyan(), "]: ", message);
        }
    }
}
