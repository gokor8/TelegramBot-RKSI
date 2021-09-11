using Microsoft.Win32;
using RKSI_bot.WindowsInteractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot
{
    class AutoRunWindows : IAutoRun
    {
        public string Path { get; set; } = System.IO.Path.GetFullPath(@".");

        public void SetToAutoRun()
        {
            using (RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true))
            {
                regkey.SetValue("Rksi", Path + @"\RKSI_bot.exe");
            }
        }
    }
}
