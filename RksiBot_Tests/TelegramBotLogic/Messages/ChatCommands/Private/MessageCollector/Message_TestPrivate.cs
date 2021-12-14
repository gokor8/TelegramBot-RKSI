using Xunit;
using RKSI_bot.Comand_Message.Objects.Commands_Group_Objects;
using RKSI_bot;

namespace RksiBot_Tests.TelegramBotLogic.Messages.ChatCommands.Private.NudeMessage
{
    public class Message_TestPrivate
    {
        public Message_TestPrivate()
        {
            new TelegramBot();
        }

        [Theory]
        [InlineData("/help")]
        [InlineData("/me")]
        public void Execute_BadMessages_ReturnNull(string message)
        {
            Telegram.Bot.Types.Message messageInformation = new Telegram.Bot.Types.Message();
            messageInformation.Text = message;
            messageInformation.Chat = new Telegram.Bot.Types.Chat() { Id = 399418047 };

            var messageFactory = new Message();

            messageFactory.Execute(messageInformation);

            Assert.False(messageFactory?.IsExecuted);
        }

        [Theory]
        [InlineData("ПОКС-34")]
        public void Execute_GoodMessages_ReturnTrue(string message)
        {
            Telegram.Bot.Types.Message messageInformation = new Telegram.Bot.Types.Message();
            messageInformation.Text = message;
            messageInformation.Chat = new Telegram.Bot.Types.Chat() { Id = 399418047 };

            var messageFactory = new Message();

            messageFactory.Execute(messageInformation);

            Assert.True(messageFactory.IsExecuted);
        }

        [Theory]
        [InlineData("aboba")]
        [InlineData("aboba22")]
        public void Execute_UsualNotTriggeredMessages_ReturnTrue(string message)
        {
            Telegram.Bot.Types.Message messageInformation = new Telegram.Bot.Types.Message();
            messageInformation.Text = message;
            messageInformation.Chat = new Telegram.Bot.Types.Chat() { Id = 399418047 };

            var messageFactory = new Message();

            messageFactory.Execute(messageInformation);

            Assert.True(messageFactory?.IsExecuted);
        }
    }
}
