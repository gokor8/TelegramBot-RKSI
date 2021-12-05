using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.Logs
{
    class LogConsole : Log
    {
        protected override void PrintLog(string message)
        {
            Console.WriteLine(message);

            IsWrited = true;
        }
    }
}
