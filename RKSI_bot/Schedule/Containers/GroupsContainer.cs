using RKSI_bot.Databases.EntityDataBase;
using RKSI_bot.Databases.EntityDataBase.Tables;
using RKSI_bot.Schedule.Containers;
using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RKSI_bot.SchedulesContainer
{
    public sealed class GroupsContainer : BaseUnitsContainer
    {
        private static readonly GroupsContainer groupsContainer = new GroupsContainer();

        private GroupsContainer() : base(new ParserGroups())
        { }

        public override void RefreshTable()
        {
            using (var context = new CollageUnitsDb())
            {
                var teachers = context.Teachers.Select(x => x.Name).ToList();

                bool IsEqual = Titels.SequenceEqual(teachers);

                if (!IsEqual)
                {
                    context.Groups.RemoveRange(context.Groups);

                    foreach (var teacher in Titels)
                        context.Groups.Add(new Group() { Name = teacher });

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

        public static GroupsContainer GetInstance()
        {
            return groupsContainer;
        }
    }
}
