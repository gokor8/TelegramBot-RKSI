using RKSI_bot.TelegramBotClasses.Messages.Objects.CommandsChannel.NudeMessage;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests.TelegramBotLogic.Messages.ChatCommands.Private.NudeMessage
{
    public class Message_Test
    {
        [Theory]
        [InlineData("/help")]
        public void MessageFactory_TestSomeMessages_ReturnedFactory(string message)
        {
            long chatId = 399418047;

            new MessageFactory().CreateMessage(message).Invoke(message, chatId);
        }
    }
}
