using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests.Schedule_Tests
{
    public class TeachersSchedule_Test
    {
        [Fact]
        public void GetGroupsHtml_NotNull()
        {
            string message = "Арутюнян М.М.";

            TeachersSchedule groupsSchedule = new TeachersSchedule();

            string schedule = groupsSchedule.GetParsedText(message).Result;

            Assert.NotNull(schedule);
        }
    }
}
