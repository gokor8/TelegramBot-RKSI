using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot
{
    class AutoRunWindows
    {
        public void SetToAutoRun()
        {
            using (RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                regkey.SetValue("Rksi", @"C:\Program Files\Visual Projects\RKSI_bot\RKSI_bot\bin\Release\netcoreapp3.1\RKSI_bot.exe");
                regkey.Close();
            }
        }
    }
}
