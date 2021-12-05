using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RksiBot_Tests.Schedule_Tests
{
    public class GroupsSchedule_Test
    {
        [Fact]
        public async Task GetParsedText_Day_NotNull()
        {
            string message = "покс-34b";

            GroupsSchedule groupsSchedule = new GroupsSchedule();

            List<string> schedule = await groupsSchedule.GetParsedText(message);

            Assert.True(schedule[0].Length > 70);
        }

        [Fact]
        public async Task GetParsedText_Week_NotNull()
        {
            string message = "покс-34b";

            GroupsSchedule groupsSchedule = new GroupsSchedule();

            List<string> schedule = await groupsSchedule.GetParsedText(message, true);

            Assert.True(schedule[0].Length > 70);
        }
    }
}
