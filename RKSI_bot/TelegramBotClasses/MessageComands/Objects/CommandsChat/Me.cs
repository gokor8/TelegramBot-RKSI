
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
        public List<string> ChatPermissions { get; set; }
        public string[] Triggers { get; set; }
        public void Execute(ref MessageEventArgs message)
        {
            long chatId = message.Message.Chat.Id;
            TelegramBot.Bot.SendTextMessageAsync(chatId, "Подождите, 3 секунды, ищу информацию в базе данных...");

            object faculty = new DataBase(message.Message.Text, message.Message.Chat.Id, new LocalPathDB("Database")).ExcecuteCommand($"SELECT Facult FROM UserTable WHERE ChatId = '{chatId}'");

            if (faculty != null)
                TelegramBot.Bot.SendTextMessageAsync(message.Message.Chat.Id, message.Message.Chat.FirstName + " " + message.Message.Chat.LastName + message.Message.Chat.Title + " : " + faculty.ToString());
            else
                TelegramBot.Bot.SendTextMessageAsync(message.Message.Chat.Id, "Ты пока не подписался на рассылку");
        }
    }
}