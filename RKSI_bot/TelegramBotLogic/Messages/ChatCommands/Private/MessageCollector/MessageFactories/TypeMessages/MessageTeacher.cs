using RKSI_bot.SchedulesContainer;
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
        private TeachersDataStore teachersContainer;
        public MessageTeacher()
        {
            teachersContainer = TeachersDataStore.GetInstance();
        }

        public bool CheckTrigger(string message)
        {
            return message.Replace(" ", "").All(c => char.IsLetter(c) || c == '.');
        }

        public void Invoke(string message, long chatId)
        {
            string foundTeacher = teachersContainer.GetTitels().FirstOrDefault(t => t.ToUpper().Replace(" ", "").Contains(message));

            if (foundTeacher != null)
                HttpRKSI.GetInstace().SendScheduleMessage(foundTeacher, chatId, new TeachersSchedule()).Wait();
            else
                TelegramBot.Bot.SendTextMessageAsync(chatId, "Такого преподавателя нету");
        }
    }
}
