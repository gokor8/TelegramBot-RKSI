using System;
using RKSI_bot;
using RKSI_bot.Databases.PathDB;
using Xunit;

namespace RksiBot_Tests
{
    public class DataBaseTest
    {
        [Fact]
        public void Test_Connect_To_KeyboardDB()
        {
            var localDBPath = new LocalPathDB("Database");

            localDBPath.PathDB = localDBPath.PathDB.Replace("RksiBot_Tests", "RKSI_bot");
            long chatId = 12;

            var userGroup = new DataBase(localDBPath);
            object aboba = userGroup.ExcecuteCommand($"SELECT UserGroup FROM ButtonTable WHERE UserChatID = '{chatId}'");
            Assert.NotNull(aboba);
        }

        [Fact]
        public void Test_Connect()
        {
            DataBase dataBase = new DataBase("Amogus", 12, new LocalPathDB("Database"));

            if (dataBase != null)
            {
                Console.WriteLine(dataBase);
            }
        }
    }
}
