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
        public void GetTitels_ReturnedNotNull()
        {
            var groupTitels = groupsContainer.GetTitels();

            Assert.NotNull(groupTitels);
        }

        [Fact]
        public void GetClearTitels_ReturnedNotNull()
        {
            string expected = "ПОКС-34";
            var groupTitels = groupsContainer.GetClearTitels();

            string foundClearGroup = groupTitels.FirstOrDefault(g=>g.Equals(expected));

            Assert.NotNull(foundClearGroup);
        }

        [Fact]
        public void GetTeacherTitels_NotWorkingSite_ReturnedNotNull()
        {
            List<string> TeacherTitels = groupsContainer.GetDataBaseUnits().Select(n => n.Name).ToList();
            // Иммитация нерабочего сайта

            Assert.NotNull(TeacherTitels);
            // Проверяю получило ли свойство корректную таблицу с преподавателями
        }

        [Fact]
        public void GetClearTitels_FindPOKC34_ReturnTrue()
        {
            string expected = "ПОКС-34";

            string actual = groupsContainer.GetClearTitels().FirstOrDefault(x=>x == "ПОКС-34");

            Assert.Equal(expected,actual);
        }
    }
}
