using RKSI_bot.Databases.EntityDataBase;
using RKSI_bot.SchedulesContainer;
using RKSI_bot.Web;
using RKSI_bot.Web.Parsing;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace RKSI_bot.Schedule.Containers
{
    public abstract class BaseUnitsContainer
    {
        private HttpRKSI httpRKSI = HttpRKSI.GetInstace();
        private List<string> Titels { get; set; }

        protected BaseUnitsContainer(IParser parser, IUnit unit)
        {
            Titels = httpRKSI.GetRecentDataArray(parser);

            if (Titels is null)
            {
                using (var context = new CollageUnitsDb())
                {
                    foreach(var )

                    Titels = units.Select(x => x.Name).ToList();
                }
            }
            else
            {
                RefreshTable();
            }
        }

        public abstract void RefreshTable();

        protected List<string> GetTitels()
        {
            return Titels;
        }
    }
}
