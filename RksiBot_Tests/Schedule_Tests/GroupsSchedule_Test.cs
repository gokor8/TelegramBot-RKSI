using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests.Schedule_Tests
{
    public class GroupsSchedule_Test
    {
        [Fact]
        public void GetGroupsHtml_NotNull()
        {
            string message = "покс-34b";

            GroupsSchedule groupsSchedule = new GroupsSchedule();

            string schedule = groupsSchedule.GetParsedText(message).Result;

            Assert.NotNull(schedule);
        }
    }
}
