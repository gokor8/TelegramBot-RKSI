using RKSI_bot.Databases.EntityDataBase;
using RKSI_bot.Databases.EntityDataBase.Tables;
using RKSI_bot.Schedule.Containers;
using RKSI_bot.Web;
using RKSI_bot.Web.Parsing;
using RKSI_bot.WindowsInteractions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace RKSI_bot.SchedulesContainer
{
    public sealed class TeachersContainer : BaseUnitsContainer
    {
        private static readonly TeachersContainer teachersContainer = new TeachersContainer();

        public List<string> TeacherTitels { get; set; }

        private TeachersContainer() : base(new ParserTeachers(), new Teacher())
        {
            TeacherTitels = GetTitels();
        }

        public override void RefreshTable()
        {
            using (var context = new CollageUnitsDb())
            {
                var teachers = context.Teachers.Select(x=>x.Name).ToList();

                bool IsEqual = TeacherTitels.SequenceEqual(teachers);
                
                if(!IsEqual)
                {
                    context.Teachers.RemoveRange(context.Teachers);

                    foreach(var teacher in TeacherTitels)
                        context.Teachers.Add(new Teacher() { Name = teacher} );

                    context.SaveChanges();
                }
            }
        }

        public static TeachersContainer GetInstance()
        {
            return teachersContainer;
        }
    }
}
