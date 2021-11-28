
using RKSI_bot.Databases.PathDB;
using RKSI_bot.ReservingObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Telegram.Bot.Args;

namespace RKSI_bot.Comand_Message.Commands_Objects
{
    class Me : ICommand
    {
        public string[] Triggers { get; set; }

        public Me(params string[] triggers)
        {
            Triggers = triggers;
        }

        public void Execute(MessageEventArgs messageInfo)
        {
            long chatId = messageInfo.Message.Chat.Id;
            TelegramBot.Bot.SendTextMessageAsync(chatId, "Подождите, 3 секунды, ищу информацию в базе данных...");

            object faculty = new DataBase(messageInfo.Message.Text, messageInfo.Message.Chat.Id, new LocalPathDB("Database")).ExcecuteCommand($"SELECT Facult FROM UserTable WHERE ChatId = '{chatId}'");

            if (faculty != null)
                TelegramBot.Bot.SendTextMessageAsync(messageInfo.Message.Chat.Id, messageInfo.Message.Chat.FirstName + " " + messageInfo.Message.Chat.LastName + messageInfo.Message.Chat.Title + " : " + faculty.ToString());
            else
                TelegramBot.Bot.SendTextMessageAsync(messageInfo.Message.Chat.Id, "Ты пока не подписался на рассылку");
        }
    }
}