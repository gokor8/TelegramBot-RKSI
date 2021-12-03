using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.Commands.Commands_Objects;
using RKSI_bot.ReservingObjects;
using Telegram.Bot.Args;
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
            var foundCommand = new GroupFactory().FindCommand(command);

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
        public void ExceciteCommands_BadCommand_ReturnedMessageInChat(string command)
        {
            Telegram.Bot.Types.Message messageInformation = new Telegram.Bot.Types.Message();
            messageInformation.Text = command;
            messageInformation.Chat = new Telegram.Bot.Types.Chat() { Id = 399418047 };

            var privateFactory = new GroupFactory();

            ICommand foundedCommand = privateFactory.FindCommand(command);
            foundedCommand.Execute(messageInformation);

            Assert.True(foundedCommand.IsExecuted);
        }
    }
}
