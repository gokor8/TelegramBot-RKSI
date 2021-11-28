using RKSI_bot.Databases.EntityDataBase;
using RKSI_bot.Databases.EntityDataBase.Tables;
using RKSI_bot.Schedule.Containers;
using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RKSI_bot.SchedulesContainer
{
    public sealed class GroupsDataStore : BaseUnitsDataStore
    {
        private static readonly GroupsDataStore groupsContainer = new GroupsDataStore();

        private GroupsDataStore() : base(new ParserGroups())
        { }

        public override void RefreshTable()
        {
            using (var context = new CollageUnitsDb())
            {
                var groups = context.Groups.Select(x => x.Name).ToList();

                bool IsEqual = Titels.SequenceEqual(groups);

                if (!IsEqual)
                {
                    context.Groups.RemoveRange(context.Groups);

                    foreach (var group in Titels)
                        context.Groups.Add(new Group() { Name = group });

                    context.SaveChanges();
                }
            }
        }

        public override IEnumerable<IUnit> GetUnits()
        {
            IEnumerable<IUnit> groups;

            using (var context = new CollageUnitsDb())
            {
                groups = context.Groups.ToList();
            }

            return groups;
        }

        public static GroupsDataStore GetInstance()
        {
            return groupsContainer;
        }
    }
}
