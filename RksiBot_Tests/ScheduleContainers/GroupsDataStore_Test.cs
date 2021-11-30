using RKSI_bot.Databases.EntityDataBase;
using RKSI_bot.SchedulesContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RksiBot_Tests.ScheduleContainers_Tests
{
    public class GroupsDataStore_Test
    {

        GroupsDataStore groupsContainer;
        public GroupsDataStore_Test()
        {
            groupsContainer = GroupsDataStore.GetInstance();
        }

        [Fact]
        public void RefreshDataBase_WithClearDb_ReturnedTrue()
        {
            using (var context = new CollageUnitsDb())
            {
                context.Groups.RemoveRange(context.Groups);
            }
            // Отчищаю таблицу

            using (var context = new CollageUnitsDb())
            {
                List<string> teachersDb = context.Groups.Select(x => x.Name).ToList();

                Assert.True(groupsContainer.GetTitels().SequenceEqual(teachersDb));
            }
            // Смотрю. правильно ли записалась она из спарсенных с сайта преподавателей
        }

        [Fact]
        public void GetTitels_ReturnedNotNull()
        {
            Assert.NotNull(groupsContainer.GetTitels());
        }

        [Fact]
        public void GetTeachersfromDataBase_NotWorkingSite_ReturnedNotNull()
        {
            List<string> TeacherTitels = groupsContainer.GetDataBaseUnits().Select(n => n.Name).ToList();
            // Иммитация нерабочего сайта

            Assert.NotNull(TeacherTitels);
            // Проверяю получило ли свойство корректную таблицу с преподавателями
        }

        [Fact]
        public void GetClearTitels_POKC34_ReturnTrue()
        {
            string expected = "ПОКС-34";

            string actual = groupsContainer.GetClearTitels().FirstOrDefault(x=>x == "ПОКС-34");

            Assert.Equal(expected,actual);
        }
    }
}
