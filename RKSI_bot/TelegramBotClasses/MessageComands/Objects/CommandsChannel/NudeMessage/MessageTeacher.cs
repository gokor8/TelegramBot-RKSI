using RKSI_bot.Web;
using RKSI_bot.Web.Https;
using RKSI_bot.Web.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage
{
    public class MessageTeacher : IMessageType
    {
        public bool CheckTrigger(string message)
        {
            return message.Replace(" ", "").All(c => char.IsLetter(c) || c == '.');
        }

        public void Invoke(string message, long chatId)
        {
            HttpRKSI.SendScheduleMessage(message, chatId, new ParsingTeachers(new TeachersRequest())).Wait();
        }
    }
}
