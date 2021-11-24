using RKSI_bot.Databases.EntityDataBase;
using RKSI_bot.Web;
using RKSI_bot.Web.Parsing;
using System.Collections.Generic;
using System.Linq;

namespace RKSI_bot.Schedule.Containers
{
    public abstract class BaseUnitsContainer
    {
        private HttpRKSI httpRKSI = HttpRKSI.GetInstace();
        public List<string> Titels { get; private set; }

        protected BaseUnitsContainer(IParser parser)
        {
            Titels = httpRKSI?.GetRecentDataArray(parser);

            if (Titels is null)
            {
                Titels = GetUnits().Select(x => x.Name).ToList();
            }
            else
            {
                RefreshTable();
            }
        }

        public abstract IEnumerable<IUnit> GetUnits();
        public abstract void RefreshTable();
    }
}
