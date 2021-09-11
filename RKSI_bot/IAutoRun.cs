using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.WindowsInteractions
{
    interface IAutoRun
    {
        string Path { get; set; }
        public void SetToAutoRun();
    }
}
