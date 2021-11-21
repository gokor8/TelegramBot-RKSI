using RKSI_bot.Databases.PathDB;
using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramBotClasses.Keyboards.UserKeyboard;
using RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage;
using RKSI_bot.TelegramBotClasses.Messages.Objects.CommandsChannel.NudeMessage;
using RKSI_bot.TelegramElements;
using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace RKSI_bot.Comand_Message.Objects.Commands_Group_Objects
{
    internal class Message : ICommand
    {
        public List<string> ChatPermissions { get; set; }
        public string[] Triggers { get; set; }

        public void Execute(ref MessageEventArgs chatInformation)
        {
            string message = Regex.Replace(chatInformation.Message.Text, @"\s+", " ");
            long chatId = chatInformation.Message.Chat.Id;

            new MessageFactory().CreateMessage(message).Invoke(message, chatId);
        }
    }
}