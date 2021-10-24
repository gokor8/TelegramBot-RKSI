using RKSI_bot.Databases.PathDB;
using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramBotClasses.Keyboards.UserKeyboard;
using RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage;
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

        private List<IMessageType> messageTypes = new List<IMessageType>();

        public Message()
        {
            RegistrateMessageTypes();
        }

        public void Execute(ref MessageEventArgs chatInformation)
        {
            string mesage = Regex.Replace(chatInformation.Message.Text, @"\s+", " ");
            long chatId = chatInformation.Message.Chat.Id;

            foreach(var messageType in messageTypes)
            {
                bool isThisTrigger = messageType.CheckTrigger(mesage);
                if (isThisTrigger)
                    messageType.Invoke(mesage, chatId);
            }
        }

        private void RegistrateMessageTypes()
        {
            messageTypes.Add(new MessageGroup(new LocalPathDB("Database")));
            messageTypes.Add(new MessageTeacher());
        }
    }
}