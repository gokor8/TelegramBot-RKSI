using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace RKSI_bot
{
    class DataBase
    {
        public string PathToDB = System.IO.Path.GetFullPath(@"..\..\..\") + $@"Databases\";

        private List<string> typesSqlCommand = new List<string>() { "Create", "Alter", "Drop", "Insert", "Update", "Delete" };
        private string textMessage;
        private long chatId;

        public DataBase(string nameDB = "Database") 
        {
            PathToDB += nameDB;
        }

        public DataBase(string textMessage, long chatId, string nameDB = "Database")
        {
            PathToDB += nameDB;
            this.textMessage = textMessage;
            this.chatId = chatId;
        }
        public object ExcecuteCommand(string command)
        {
            using (SqlConnection connection = CheckConnection())
            {
                if (connection is null)
                    throw new Exception("class DataBase:27 |Connection = null|");

                SqlCommand CommandInvoker = new SqlCommand(command, connection);
                object objChanges;

                //string amogus = typesSqlCommand.FirstOrDefault(cmd => command.Contains(cmd.ToUpper())); Проверка

                if (typesSqlCommand.FirstOrDefault(cmd => command.Contains(cmd.ToUpper())) is null)
                    objChanges = CommandInvoker?.ExecuteScalar();
                else
                    objChanges = CommandInvoker.ExecuteNonQuery();

                return objChanges;
            }
        }

        public bool GetBool(object value)
        {
            if (value != null)
                return Convert.ToInt32(value) == 1 ? true : false;
            else
                return false;
        }

        public async Task ScheduleDataBase(string attribute, string table = "ttable")
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
                        await HttpRKSI.SendScheduleMessage(record[1].ToString(), Convert.ToInt64(record[0]));
                    }catch(Exception exc)
                    { Console.WriteLine(exc); }
                }
            }
        }
        private SqlConnection CheckConnection()
        {
            SqlConnection Connection = new SqlConnection();

            Connection.ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={PathToDB}.mdf;Integrated Security=True";

            if (Connection.State == ConnectionState.Closed)
                try
                {
                    Connection.Open();
                }
                catch (SqlException)
                {
                    TelegramBot.Bot.SendTextMessageAsync(chatId, "Произошла ошибка с подключением к Базе данных, повторите попытку позднее");
                    return null;
                }

            return Connection;
        }
    }
}
