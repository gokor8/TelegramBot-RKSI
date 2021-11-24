using RKSI_bot.Databases.EntityDataBase;
using RKSI_bot.SchedulesContainer;
using RKSI_bot.Web;
using RKSI_bot.Web.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RksiBot_Tests.ScheduleContainers_Tests
{
    public class TeacherContainer_Test
    {
        public TeacherContainer_Test()
        {
            using (var context = new CollageUnitsDb())
            {
                context.Teachers.RemoveRange(context.Teachers);
            }
        }

        [Fact]
        public void Test_TeachersList_ReturnEqualsLists()
        {
            TeachersContainer teachersContainer = TeachersContainer.GetInstance();

            using(var context = new CollageUnitsDb())
            {
                List<string> teachersDb = context.Teachers.Select(x => x.Name).ToList();

                Assert.True(teachersContainer.TeacherTitels.SequenceEqual(teachersDb));
            }
        }

        [Fact]
        public void Test_GetTeacherListFromDataBase_NotNull()
        {
            List<string> TeacherTitels = null;

            if (TeacherTitels is null)
            {
                using (var context = new CollageUnitsDb())
                {
                    TeacherTitels = context.Teachers.Select(x => x.Name).ToList();
                }
            }

            Assert.NotNull(TeacherTitels);

        }
    }
}
