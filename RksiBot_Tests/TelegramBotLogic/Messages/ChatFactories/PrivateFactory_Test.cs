using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.Commands.Commands_Objects;
using RKSI_bot.ReservingObjects;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace RKSI_bot.TelegramBotClasses.Messages.ChatFactories
{
    public class PrivateFactory_Test
    {
        [Fact]
        public void FindCommand_CommandHelp_ReturnedMessageInChat()
        {
            string message = "";

            new GroupFactory().FindCommand(message);
        }
    }
}
