using RKSI_bot.SchedulesContainer;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests
{
    public class MessageGroupTest
    {
        [Fact]
        public void Teachers_Test()
        {
            Assert.NotNull(Teachers.TeacherTitels);
        }
    }
}
