using RKSI_bot.Databases.PathDB;
using RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.TelegramBotClasses.Messages.Objects.CommandsChannel.NudeMessage
{
    public class MessageFactory
    {
        private IMessageType[] messageTypes;

        public MessageFactory()
        {
            messageTypes = new IMessageType[] 
            { 
                new MessageGroup(new LocalPathDb("DataBase")), 
                new MessageTeacher()
            };
        }

        public IMessageType CreateMessage(string trigger)
        {
            foreach (var messageType in messageTypes)
            {
                bool isThisTrigger = messageType.CheckTrigger(trigger);

                if (isThisTrigger)
                    return messageType;
            }

            return null;
        }
    }
}
