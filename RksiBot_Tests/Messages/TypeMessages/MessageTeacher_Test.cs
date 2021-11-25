﻿using RKSI_bot;
using RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests
{
    public class MessageTeacher_Test
    {
        private TelegramBot telegramBot;
        public MessageTeacher_Test()
        {
            telegramBot = new TelegramBot();
        }

        [Fact]
        public void TeacherRequest_Test()
        {
            string message = "Арутюнян М.М.";
            long chatID = 399418047;

            var teacher = new MessageTeacher();

            teacher.Invoke(message,chatID);
        }
    }
}