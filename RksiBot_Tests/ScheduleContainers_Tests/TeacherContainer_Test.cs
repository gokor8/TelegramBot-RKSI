using RKSI_bot.SchedulesContainer;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests.ScheduleContainers_Tests
{
    public class TeacherContainer_Test
    {
        [Fact]
        public void Test_TeachersList_NotNull()
        {
            Assert.NotNull(TeachersContainer.TeacherTitels);
        }
    }
}
