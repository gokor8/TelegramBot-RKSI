using RKSI_bot.SchedulesContainer;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests
{
    public class Teachers_Test
    {
        [Fact]
        public void TeachersTitle_Get_ReturnedArray()
        {
            Assert.NotNull(Teachers.TeacherTitels);
        }
    }
}
