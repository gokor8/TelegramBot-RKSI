using RKSI_bot;
using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.TelegramBotLogic.Messages;
using System.Threading.Tasks;
using Xunit;

namespace RksiBot_Tests.TelegramBotLogic.Messages.ChatCommands.Private
{
    public class SpamCommand_Test
    {
        [Theory]
        [InlineData("покс-34")]
        public async Task Subscribe_AddGroups_ReturnTrue(string message)
        {
            new TelegramBot();
            long chatId = 399418047;
            var dataStore = SpamDataStore.GetInstace().MessageIds;
            dataStore.Add(chatId);

            SpamSubcribe spamSubcribe = new SpamSubcribe();

            await spamSubcribe.Subscribe(message, chatId);

            Assert.True(spamSubcribe.IsSubcribe);
        }
    }
}
