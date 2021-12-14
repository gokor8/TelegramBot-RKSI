using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.Commands.Commands_Objects;
using RKSI_bot.ReservingObjects;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace RKSI_bot.TelegramBotClasses.Messages.ChatFactories
{
    public class GroupFactory_Test
    {
        public GroupFactory_Test()
        {
            new TelegramBot();
        }

        [Theory]
        [InlineData("/help")]
        [InlineData("/me")]
        [InlineData("/list")]
        [InlineData("/start")]
        [InlineData("/group")]
        [InlineData("пары ПОКС-34")]
        [InlineData("пары Amogus")]
        public void FindCommand_BadCommand_ReturnedMessageInChat(string command)
        {
            Message messageInformation = buildTelegramMessage(command, 399418047);

            var foundCommand = new GroupFactory().FindCommand(messageInformation);

            Assert.NotNull(foundCommand);
        }

        [Theory]
        [InlineData("/help")]
        [InlineData("/me")]
        [InlineData("/list")]
        [InlineData("/start")]
        [InlineData("/group")]
        [InlineData("пары ПОКС-34")]
        [InlineData("пары Amogus")]
        public void ExcecuteCommands_BadCommand_ReturnedMessageInChat(string command)
        {
            Message messageInformation = buildTelegramMessage(command, 399418047);

            var privateFactory = new GroupFactory();

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
