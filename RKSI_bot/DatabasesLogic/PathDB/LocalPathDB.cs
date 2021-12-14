using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.Databases.PathDB
{
    public class LocalPathDb : IPathDB
    {
        public string PathDB { get; set; } = System.IO.Path.GetFullPath(@"..\..\..\..\") + $@"RKSI_bot\Databases\";
        public LocalPathDb()
        {

        }
        public LocalPathDb(string nameDB)
        {
            PathDB += nameDB;
        }
    }
}
