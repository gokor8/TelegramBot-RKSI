using RKSI_bot.Databases.EntityDataBase;
using RKSI_bot.Databases.EntityDataBase.Tables;
using RKSI_bot.Schedule.Containers;
using RKSI_bot.Web.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RKSI_bot.SchedulesContainer
{
    public sealed class TeachersContainer : BaseUnitsContainer
    {
        private static readonly TeachersContainer teachersContainer = new TeachersContainer();

        private TeachersContainer() : base(new ParserTeachers())
        { }

        public override void RefreshTable()
        {
            using (var context = new CollageUnitsDb())
            {
                var teachers = context.Teachers.Select(x=>x.Name).ToList();

                bool IsEqual = Titels.SequenceEqual(teachers);
                
                if(!IsEqual)
                {
                    context.Teachers.RemoveRange(context.Teachers);

                    foreach(var teacher in Titels)
                        context.Teachers.Add(new Teacher() { Name = teacher} );

                    context.SaveChanges();
                }
            }
        }

        public override IEnumerable<IUnit> GetUnits()
        {
            IEnumerable<IUnit> teachers;

            using (var context = new CollageUnitsDb())
            {
                teachers = context.Teachers.ToList();
            }

            return teachers;
        }

        public static TeachersContainer GetInstance()
        {
            return teachersContainer;
        }
    }
}
