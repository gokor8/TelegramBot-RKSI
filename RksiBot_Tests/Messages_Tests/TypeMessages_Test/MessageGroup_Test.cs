using RKSI_bot;
using RKSI_bot.Databases.PathDB;
using RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests.Messages_Tests.TypeMessages_Test
{
    public class MessageGroup_Test
    {
        private TelegramBot telegramBot;
        public MessageGroup_Test()
        {
            telegramBot = new TelegramBot();
        }

        [Fact]
        public void GroupRequest_Test()
        {
            string message = "ПОКС-34b";
            long chatID = 399418047;

            var teacher = new MessageGroup(new LocalPathDB("Database"));

            teacher.Invoke(message, chatID);
        }
    }
}
