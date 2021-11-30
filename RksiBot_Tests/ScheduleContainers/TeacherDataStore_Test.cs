using RKSI_bot.Databases.EntityDataBase;
using RKSI_bot.SchedulesContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RksiBot_Tests.ScheduleContainers_Tests
{
    public class TeacherDataStore_Test
    {
        TeachersDataStore teachersContainer;
        public TeacherDataStore_Test()
        {
            teachersContainer = TeachersDataStore.GetInstance();
        }

        [Fact]
        public void RefreshDataBase_WithClearDb_ReturnedTrue()
        {
            using (var context = new CollageUnitsDb())
            {
                context.Teachers.RemoveRange(context.Teachers);
            }
            // Отчищаю таблицу

            using (var context = new CollageUnitsDb())
            {
                List<string> teachersDb = context.Teachers.Select(x => x.Name).ToList();

                Assert.True(teachersContainer.GetTitels().SequenceEqual(teachersDb));
            }
            // Смотрю. правильно ли записалась она из спарсенных с сайта преподавателей
        }

        [Fact]
        public void GetTitels_ReturnedNotNull()
        {
            Assert.NotNull(teachersContainer.GetTitels());
        }

        [Fact]
        public void GetTeachersfromDataBase_NotWorkingSite_ReturnedNotNull()
        {
            List<string> TeacherTitels = teachersContainer.GetDataBaseUnits().Select(n=>n.Name).ToList();
            // Иммитация нерабочего сайта

            Assert.NotNull(TeacherTitels);
            // Проверяю получило ли свойство корректную таблицу с преподавателями
        }
    }
}
