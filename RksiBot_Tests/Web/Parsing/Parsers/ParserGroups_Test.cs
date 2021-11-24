using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests.Web.Parsing.Parsers
{
    public class ParserGroups_Test
    {
        [Fact]
        public void GetParsedList_Test_ReturnDatasList()
        {
            var httpRKSI = HttpRKSI.GetInstace();

            var Groups = httpRKSI.GetRecentDataArray(new ParserGroups());

            Assert.NotEmpty(Groups);
        }
    }
}
