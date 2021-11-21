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
            var excelTeachers = HttpRKSI.GetInstace().GetRecentDataArray(new ParserTeachers());

            Assert.NotNull(excelTeachers);
            new ExcelGroups(@"C:\Users\gvala\OneDrive\Рабочий стол\Groups.xlsx").SetDataExcel(excelTeachers);
        }

        [Fact]
        public void SetGroupData()
        {
            var excelGroups = HttpRKSI.GetInstace().GetRecentDataArray(new ParserGroups());

            Assert.NotNull(excelGroups);
            new ExcelGroups(@"C:\Users\gvala\OneDrive\Рабочий стол\Groups.xlsx").SetDataExcel(excelGroups);
        }
    }
}
