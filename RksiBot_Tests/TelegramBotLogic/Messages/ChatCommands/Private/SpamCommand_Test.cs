using RKSI_bot;
using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.TelegramBotLogic.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RksiBot_Tests.TelegramBotLogic.Messages.ChatCommands.Private
{
    public class SpamCommand_Test
    {
        [Theory]
        [InlineData("покс-34")]
        [InlineData("покс-34b")]
        public async Task Subscribe_TestInlineData_ReturnedSubscribeMessage(string message)
        {
            message = message.Replace("b","").Replace("w","");

            new TelegramBot();
            var dataStore = SpamDataStore.GetInstace().MessageIds;

            long chatId = 399418047;

            dataStore.Add(chatId);
            await new SpamCommand().Subscribe(message, chatId);
        }
    }
}
