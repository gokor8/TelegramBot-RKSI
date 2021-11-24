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

        public List<string> GroupsTitels { get; set; }

        private GroupsContainer() : base(new ParserGroups(), new Group())
        {
            GroupsTitels = GetTitels();
        }

        public override void RefreshTable()
        {
            using (var context = new CollageUnitsDb())
            {
                var groups = context.Teachers.Select(x => x.Name).ToList();

                bool IsEqual = GroupsTitels.SequenceEqual(groups);

                if (!IsEqual)
                {
                    context.Entry(GroupsTitels).Reload();
                }
            }
        }

        public static GroupsContainer GetInstance()
        {
            return groupsContainer;
        }
    }
}
