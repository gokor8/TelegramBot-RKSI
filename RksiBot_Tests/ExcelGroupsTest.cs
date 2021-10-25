using RKSI_bot.Web;
using RKSI_bot.Web.Https;
using RKSI_bot.Web.Parsing;
using RKSI_bot.WindowsInteractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests
{
    public class ExcelGroupsTest
    {
        [Fact]
        public void TestAllDatas()
        {
            SetGroupData();
            SetTeacherData();
        }
        [Fact]
        public void SetTeacherData()
        {
            var excelTeachers = new ParsingTeachers(new TeachersRequest()).GetRecentDataArray();
            new ExcelGroups(@"D:\Users\gzaly\OneDrive\Рабочий стол\Groups.xlsx").SetDataExcel(excelTeachers);
        }

        [Fact]
        public void SetGroupData()
        {
            var excelGroups = new ParsingGroups(new GroupsRequset()).GetRecentDataArray();
            new ExcelGroups(@"D:\Users\gzaly\OneDrive\Рабочий стол\Groups.xlsx").SetDataExcel(excelGroups);
        }
    }
}
