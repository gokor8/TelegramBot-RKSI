﻿using RKSI_bot.WindowsInteractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.SchedulesContainer
{
    public sealed class Teachers
    {
        public static readonly string[] TeacherTitels = new ExcelGroups(@"C:\Users\Григорий\Desktop\Groups.xlsx").GetDataExcel(false)[1][0];
    }
}
