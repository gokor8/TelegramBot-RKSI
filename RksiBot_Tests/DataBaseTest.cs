using System;
using RKSI_bot;
using RKSI_bot.Databases.PathDB;
using Xunit;

namespace RksiBot_Tests
{
    public class DataBaseTest
    {
        [Fact]
        public void Connect_ToKeyboardDB_ReturnNotNullExcecute()
        {
            var localDBPath = new LocalPathDB("Database");

            localDBPath.PathDB = localDBPath.PathDB.Replace("RksiBot_Tests", "RKSI_bot");
            long chatId = 12;

            var userGroup = new DataBase(localDBPath);
            object aboba = userGroup.ExcecuteCommand($"SELECT Facult FROM ButtonFacultTable WHERE ChatId = '{chatId}'");
            Assert.NotNull(aboba);
        }

        [Fact]
        public void TestConnection_ReturnNoException()
        {
            DataBase dataBase = new DataBase("Amogus", 12, new LocalPathDB("Database"));

            if (dataBase != null)
            {
                Console.WriteLine(dataBase);
            }
        }
    }
}
