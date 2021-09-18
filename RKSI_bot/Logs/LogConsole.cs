using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.Logs
{
    class LogConsole : Log
    {
        public override void PrintLog(string message)
        {
            Console.WriteLine(message);
        }
    }
}
