﻿using RKSI_bot.Groups;
using System;
using System.Linq;

namespace RKSI_bot
{
    static class GroupsContainer
    {
        public static string[] Groups;

        static GroupsContainer()
        {
            Groups = new ExcelGroups(@"D:\Users\gzaly\OneDrive\Рабочий стол\Groups.xlsx").GetDataExcel(false)[0][0];
        }

        public static bool IsConsistGroups(string message)
        {
            foreach (var group in Groups)
            {
                if (message.ToUpper().Trim().Equals(group))
                {
                    return true;
                }
            }

            return false;
        }
    }
}