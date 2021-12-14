using RKSI_bot.Databases.EntityDataBase;
using RKSI_bot.Web;
using RKSI_bot.Web.Parsing;
using System.Collections.Generic;
using System.Linq;

namespace RKSI_bot.Schedule.UnitDataStores
{
    public abstract class UnitsDataStore
    {
        private HttpRKSI httpRKSI = HttpRKSI.GetInstace();
        protected List<string> Titels { get; set; }

        protected UnitsDataStore(IParser parser)
        {
            Titels = httpRKSI?.GetRecentDataArray(parser);

            if (Titels is null)
            {
                Titels = GetDataBaseUnits().Select(x => x.Name).ToList();
            }
            else
            {
                RefreshTable();
            }
        }

        public List<string> GetTitels()
        {
            return Titels;
        }
        public abstract IEnumerable<IUnit> GetDataBaseUnits();
        public abstract void RefreshTable();
    }
}
