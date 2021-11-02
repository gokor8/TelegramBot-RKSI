using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.Databases.PathDB
{
    public class LocalPathDB : IPathDB
    {
        public string PathDB { get; set; } = System.IO.Path.GetFullPath(@"..\..\..\") + $@"Databases\LocalDBs\";

        public LocalPathDB(string nameDB)
        {
            PathDB += nameDB;
        }
    }
}
