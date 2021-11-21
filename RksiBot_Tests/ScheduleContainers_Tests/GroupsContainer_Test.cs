using RKSI_bot.SchedulesContainer;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests.ScheduleContainers_Tests
{
    public class GroupsContainer_Test
    {
        [Fact]
        public void Test_GroupsList_NotNull()
        {
            Assert.NotNull(GroupsContainer.GroupTitles);
        }
    }
}
