using RKSI_bot.Databases.EntityDataBase.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace RKSI_bot.Databases.EntityDataBase
{
    public class CollageUnitsDb : DbContext
    {
        private const string Path_Db = @"C:\Users\gvala\OneDrive\Рабочий стол\TelegramBot-RKSI\RKSI_bot\Databases\LocalDBs\DatabaseCollegeUnits.mdf";
        private const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+ Path_Db + ";Integrated Security=True";

        public CollageUnitsDb() : base(CONNECTION_STRING)
        {

        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
