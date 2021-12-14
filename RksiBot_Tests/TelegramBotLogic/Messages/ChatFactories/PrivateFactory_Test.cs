using RKSI_bot.ReservingObjects;
using Telegram.Bot.Types;
using Xunit;

namespace RKSI_bot.TelegramBotClasses.Messages.ChatFactories
{
    public class PrivateFactory_Test
    {
        public PrivateFactory_Test()
        {
            new TelegramBot();
        }

        [Theory]
        [InlineData("/help")]
        [InlineData("/me")]
        [InlineData("/list")]
        [InlineData("/start")]
        [InlineData("/group")]
        [InlineData("вайяяя")]
        [InlineData("ПОКС-34")]
        [InlineData("Amogus")]
        public void FindCommand_BadCommand_ReturnedMessageInChat(string command)
        {
            Message messageInformation = buildTelegramMessage(command, 399418047);

            var foundCommand = new PrivateFactory().FindCommand(messageInformation);

            Assert.NotNull(foundCommand);
        }

        [Theory]
        [InlineData("/help")]
        [InlineData("/me")]
        [InlineData("/list")]
        [InlineData("/start")]
        [InlineData("/group")]
        [InlineData("вайяяя")]
        [InlineData("ПОКС-34")]
        [InlineData("Amogus")]
        public void ExecuteCommands_BadCommand_ReturnedMessageInChat(string command)
        {
            Message messageInformation = buildTelegramMessage(command, 399418047);

            var privateFactory = new PrivateFactory();

            ICommand foundedCommand = privateFactory.FindCommand(messageInformation);
            foundedCommand.Execute(messageInformation);

            Assert.True(foundedCommand.IsExecuted);
        }

        private Message buildTelegramMessage(string message, long chatId)
        {
            Message messageInformation = new Message();
            messageInformation.Text = message;
            messageInformation.Chat = new Chat() { Id = chatId };

            return messageInformation;
        }
    }
}
