using RKSI_bot.Databases.PathDB;
using RKSI_bot.SchedulesContainer;
using RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests
{
    public class TitelsContainersTest
    {
        [Fact]
        public void CheckGroupTrigger_Test()
        {
            string message = "покс-34b";
            var localDBPath = new LocalPathDB("Database");
            localDBPath.PathDB = localDBPath.PathDB.Replace("RksiBot_Tests", "RKSI_bot");

            bool result = new MessageGroup(localDBPath).CheckTrigger(message);

            Assert.True(result);
        }

        [Fact]
        public void CheckTeacherTrigger_Test()
        {
            string message = "Арутюнян";

            bool result = new MessageTeacher().CheckTrigger(message);

            Assert.True(result);
        }
    }
}
