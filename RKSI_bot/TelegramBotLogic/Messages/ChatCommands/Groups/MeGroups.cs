using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.Databases.PathDB;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace RKSI_bot.TelegramBotLogic.Messages.ChatCommands.Groups
{
    class MeGroups : Me
    {
        public MeGroups(params string[] triggers) : base(triggers)
        {

        }
        public override void Execute(MessageEventArgs messageInfo)
        {
            long chatId = messageInfo.Message.Chat.Id;
            TelegramBot.Bot.SendTextMessageAsync(chatId, "Подождите, 3 секунды, ищу информацию в базе данных...");

            object faculty = new DataBase(messageInfo.Message.Text, messageInfo.Message.Chat.Id, new LocalPathDB("Database")).ExcecuteCommand($"SELECT Facult FROM UserTable WHERE ChatId = '{chatId}'");

            if (faculty != null)
                TelegramBot.Bot.SendTextMessageAsync(messageInfo.Message.Chat.Id, messageInfo.Message.Chat.Title + " : " + faculty.ToString());
            else
                TelegramBot.Bot.SendTextMessageAsync(messageInfo.Message.Chat.Id, "Ты пока не подписался на рассылку");
        }
    }
}
