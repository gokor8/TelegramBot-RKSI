using RKSI_bot.Databases.PathDB;
using RKSI_bot.Web;
using RKSI_bot.Web.Https;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RKSI_bot.Databases
{
    class ScheduleDB : DataBase
    {
        public ScheduleDB(IPathDB pathDB) : base(pathDB) { }

        public async Task SendScheduleFromDB(string attribute, string table = "ttable")
        {
            using (SqlConnection connection = CheckConnection())
            {
                string sqlCommand = $"SELECT {attribute}, Facult FROM {table};";
                SqlCommand command = new SqlCommand(sqlCommand, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    IDataRecord record = reader;
                    Console.WriteLine(record[0].ToString() + " : " + record[1].ToString());
                    try
                    {
                        await HttpRKSI.SendScheduleMessage(record[1].ToString(), Convert.ToInt64(record[0]), new GroupsSchedule());
                    }
                    catch (Exception exc)
                    { Console.WriteLine(exc); }
                }
            }
        }
    }
}
