using RKSI_bot.Databases.EntityDataBase;
using RKSI_bot.Databases.EntityDataBase.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xunit;

namespace RksiBot_Tests.DB_Tests.Entity_Test
{
    public class CollageUnitsBD_Test
    {
        [Fact]
        public void SetGroups_ReturnSetData()
        {
            using(var context = new CollageUnitsDb())
            {
                var group = new Group()
                {
                    Name = "Покс-34b"
                };

                context.Groups.Add(group);
                context.SaveChanges();

                var addedGroup = context.Groups.FirstOrDefault(x => x.Name.Equals("Покс-34b"));

                Assert.Equal("Покс-34b", addedGroup.Name);

                Debug.WriteLine(addedGroup.Id);
            }
        }

        [Fact]
        public void GetGroups_ReturnData()
        {
            using (var context = new CollageUnitsDb())
            {
                foreach(var group in context.Groups)
                    Debug.WriteLine(group.Id + ": " + group.Name);
            }
        }

        [Fact]
        public void ReplaceAllGroups_ReturnData()
        {
            using (var context = new CollageUnitsDb())
            {
                foreach (var group in context.Groups)
                    group.Name = "Amogus";

                foreach (var group in context.Groups)
                {
                    Assert.Equal("Amogus", group.Name);
                    Debug.WriteLine(group.Id + ": " + group.Name);
                }

                context.SaveChanges();
            }
        }
    }
}
