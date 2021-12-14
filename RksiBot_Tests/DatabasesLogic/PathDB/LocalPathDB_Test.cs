using RKSI_bot.Databases.PathDB;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests.DB_Tests.PathDB
{
    public class LocalPathDB_Test
    {
        [Fact]
        public void GetLocalPathDB_ReturnRKSI_botDataBase()
        {
            LocalPathDb localPathDB = new LocalPathDb("Database");

            string excpectedPathDB = @"C:\Users\gvala\OneDrive\Рабочий стол\TelegramBot-RKSI\RKSI_bot\Databases\Database";

            Assert.Equal(excpectedPathDB, localPathDB.PathDB);
        } 
    }
}
