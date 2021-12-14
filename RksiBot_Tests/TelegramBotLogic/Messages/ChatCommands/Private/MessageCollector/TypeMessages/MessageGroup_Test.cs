using RKSI_bot;
using RKSI_bot.Databases.PathDB;
using RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage;
using Xunit;

namespace RksiBot_Tests.TelegramBotLogic.Messages.ChatCommands.Private.MessageCollector.TypeMessages
{
    public class MessageGroup_Test
    {
        public MessageGroup_Test()
        {
            new TelegramBot();
        }

        [Fact]
        public void Execute_GroupMessage_ReturnIsSendTrue()
        {
            string group = "ПОКС-34";
            long chatId = -419002292;
            var messageGroup = new MessageGroup(new LocalPathDb("DataBase"));

            messageGroup.Invoke(group, "пары ", chatId);

            Assert.True(messageGroup.IsSended);
        }

        [Fact]
        public void Execute_GroupMessage_ReturnIsSendFalse()
        {
            string group = "ПОКС-341";
            long chatId = -419002292;
            var messageGroup = new MessageGroup(new LocalPathDb("DataBase"));

            messageGroup.Invoke(group, "пары ", chatId);

            Assert.False(messageGroup.IsSended);
        }

        [Fact]
        public void Execute_PrivateMessage_ReturnIsSendTrue()
        {
            string group = "ПОКС-34";
            long chatId = 399418047;
            var messageGroup = new MessageGroup(new LocalPathDb("DataBase"));

            messageGroup.Invoke(group, "", chatId);

            Assert.True(messageGroup.IsSended);
        }

        [Theory]
        [InlineData("ПОКС-341")]
        [InlineData("пары ПОКС-34")]
        public void Execute_PrivateMessage_ReturnIsSendFalse(string group)
        {
            long chatId = 399418047;
            var messageGroup = new MessageGroup(new LocalPathDb("DataBase"));

            messageGroup.Invoke(group, "", chatId);

            Assert.False(messageGroup.IsSended);
        }
    }
}
