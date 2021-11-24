﻿using RKSI_bot.SchedulesContainer;
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
        private TeachersContainer teachersContainer;
        public MessageTeacher()
        {
            teachersContainer = TeachersContainer.GetInstance();
        }

        public bool CheckTrigger(string message)
        {
            return message.Replace(" ", "").All(c => char.IsLetter(c) || c == '.');
        }

        public void Invoke(string message, long chatId)
        {
            foreach (var teacher in teachersContainer.Titels)
            {
                if (teacher.Contains(message))
                    message = teacher;
            }

            HttpRKSI.GetInstace().SendScheduleMessage(message, chatId, new TeachersSchedule()).Wait();
        }
    }
}
