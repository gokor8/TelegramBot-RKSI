using Microsoft.Extensions.Configuration;
using RKSI_bot.Databases.EntityDataBase.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace RKSI_bot.Databases.EntityDataBase
{
    public class CollageUnitsDb : DbContext
    {
        private static readonly IConfigurationSection _config = StartupConfig.GetInstance().Configuration.GetSection("ConnectionStrings");

        private static readonly string _connectionString = $@"{_config["FirstStringConnection"]}
            DatabaseCollegeUnits.mdf{_config["LastStringConnection"]}";

        public CollageUnitsDb() : base(_connectionString)
        {

        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
