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

        [Theory]
        [InlineData("Покс-34")]
        [InlineData("Покс-34b")]
        public void Invoke_GroupMessage_ReturnMessage(string message)
        {
            message = message.Replace("b", "").Replace("w", "");

            long chatID = 399418047;

            var group = new MessageGroup(new LocalPathDB("Database"));

            group.Invoke(message.ToUpper(), chatID);
        }
    }
}
