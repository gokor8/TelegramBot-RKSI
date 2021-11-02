using RKSI_bot.WindowsInteractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.SchedulesContainer
{
    public sealed class Groups
    {
        public static readonly string[] GroupTitles = new ExcelGroups(@"C:\Users\Григорий\Desktop\Groups.xlsx").GetDataExcel(false)[0][0];
    }
}
