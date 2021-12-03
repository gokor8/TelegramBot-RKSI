using RKSI_bot.Web;
using RKSI_bot.Web.Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests.Web.Parsing.Parsers
{
    public class ParserTeachers_Test
    {
        [Fact]
        public void GetParsedList_ReturnDatasList()
        {
            var httpRKSI = HttpRKSI.GetInstace();

            var Teachers = httpRKSI.GetRecentDataArray(new ParserTeachers());

            Assert.NotEmpty(Teachers);
        }
    }
}
