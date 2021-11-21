using RKSI_bot.Databases.PathDB;
using RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.TelegramBotClasses.Messages.Objects.CommandsChannel.NudeMessage
{
    class MessageFactory
    {
        private List<IMessageType> messageTypes = new List<IMessageType>();

        public MessageFactory()
        {
            messageTypes = new List<IMessageType>() { new MessageGroup(new LocalPathDB("DataBase")), new MessageTeacher()};
        }

        public IMessageType CreateMessage(string trigger)
        {
            foreach (var messageType in messageTypes)
            {
                bool isThisTrigger = messageType.CheckTrigger(trigger);

                if (isThisTrigger)
                    return messageType;
            }

            throw new NullReferenceException("Не найден триггер");
        }
    }
}
