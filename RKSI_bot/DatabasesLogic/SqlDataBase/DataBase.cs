using RKSI_bot.Databases;
using RKSI_bot.Databases.PathDB;
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
    public class DataBase
    {
        private IPathDB _pathDb;

        private List<string> typesSqlCommand = new List<string>() { "Create", "Alter", "Drop", "Insert", "Update", "Delete" };

        private string textMessage;
        private long chatId;

        public DataBase(IPathDB pathDB) 
        {
            _pathDb = pathDB;
        }

        public DataBase(string textMessage, long chatId, IPathDB pathDB)
        {
            _pathDb = pathDB;

            this.textMessage = textMessage;
            this.chatId = chatId;
        }

        public object ExcecuteCommand(string command)
        {
            using (SqlConnection connection = Connect())
            {
                if (connection is null)
                    throw new Exception("class DataBase:27 |Connection = null|");

                SqlCommand CommandInvoker = new SqlCommand(command, connection);
                object objChanges;

                //string amogus = typesSqlCommand.FirstOrDefault(cmd => command.Contains(cmd.ToUpper())); Проверка

                if (typesSqlCommand.FirstOrDefault(cmd => command.Contains(cmd.ToUpper())) is null)
                    objChanges = CommandInvoker?.ExecuteScalar();
                else
                    objChanges = CommandInvoker?.ExecuteNonQuery();

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

        protected SqlConnection Connect()
        {
            SqlConnection Connection = new SqlConnection();

            Connection.ConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_pathDb.PathDB}.mdf;Integrated Security=True";

            if (Connection.State == ConnectionState.Closed)
                try
                {
                    Connection.Open();
                }
                catch (SqlException exc)
                {
                    TelegramBot.Bot.SendTextMessageAsync(chatId, "Произошла ошибка с подключением к Базе данных, повторите попытку позднее " + exc.Message);
                }

            return Connection;
        }
    }
}
