﻿using RKSI_bot.Schedule.TypesSchdules;
using RKSI_bot.WindowsInteractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.SchedulesContainer
{
    public sealed class TeachersContainer : BaseContainer
    {
        public static readonly string[] TeacherTitels = new ExcelGroups($@"{pathToExcel}\Groups.xlsx").GetDataExcel(false)[1][0];
    }
}
