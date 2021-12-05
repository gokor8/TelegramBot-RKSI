using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RksiBot_Tests.Schedule_Tests
{
    public class TeachersSchedule_Test
    {
        [Fact]
        public async Task GetGroupsHtml_NotNull()
        {
            string message = "Арутюнян М.М.";

            TeachersSchedule groupsSchedule = new TeachersSchedule();

            List<string> schedule = await groupsSchedule.GetParsedText(message);

            Assert.True(schedule[0].Length > 70);
        }
    }
}
