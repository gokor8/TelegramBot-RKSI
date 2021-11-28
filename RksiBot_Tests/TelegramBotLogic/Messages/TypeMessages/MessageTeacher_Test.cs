using RKSI_bot;
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

        [Theory]
        [InlineData("Арутюнян М.М.")] // Не будет работаь, так как есть пробелы. В проде они убираются
        [InlineData("АрутюнянМ.М")]
        [InlineData("Арутюнян")]
        public void Invoke_TeacherMessage_ReturnedMessage(string message)
        {
            long chatID = 399418047;

            var teacher = new MessageTeacher();

            teacher.Invoke(message.ToUpper(),chatID);
        }
    }
}
