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
        public void GetTitels_ReturnedNotNull()
        {
            var teacherTitels = teachersContainer.GetTitels();

            Assert.NotNull(teacherTitels);
        }

        [Fact]
        public void GetTeachersTitels_NotWorkingSite_ReturnedNotNull()
        {
            List<string> TeacherTitels = teachersContainer.GetDataBaseUnits().Select(n=>n.Name).ToList();
            // Иммитация нерабочего сайта

            Assert.NotNull(TeacherTitels);
            // Проверяю получило ли свойство корректную таблицу с преподавателями
        }
    }
}
