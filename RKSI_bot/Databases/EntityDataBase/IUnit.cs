using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.Databases.EntityDataBase
{
    public interface IUnit
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
