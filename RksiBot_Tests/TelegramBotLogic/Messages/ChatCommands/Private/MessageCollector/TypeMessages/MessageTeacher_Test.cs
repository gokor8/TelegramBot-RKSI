using RKSI_bot;
using RKSI_bot.Databases.PathDB;
using RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests.TelegramBotLogic.Messages.ChatCommands.Private.MessageCollector.TypeMessages
{
    public class MessageTeacher_Test
    {
        public MessageTeacher_Test()
        {
            new TelegramBot();
        }

        [Fact]
        public void Execute_GroupMessage_ReturnIsSendTrue()
        {
            string group = "Бельчич";
            long chatId = -419002292;
            var messageTeacher = new MessageTeacher();

            messageTeacher.Invoke(group.ToUpper(), "пары ", chatId);

            Assert.True(messageTeacher.IsSended);
        }

        [Fact]
        public void Execute_GroupMessage_ReturnIsSendFalse()
        {
            string group = "Бельчичих";
            long chatId = -419002292;
            var messageTeacher = new MessageTeacher();

            messageTeacher.Invoke(group.ToUpper(), "пары ", chatId);

            Assert.False(messageTeacher.IsSended);
        }

        [Theory]
        [InlineData("Бельчич")]
        public void Execute_PrivateMessage_ReturnIsSendTrue(string group)
        {
            long chatId = 399418047;
            var messageTeacher = new MessageTeacher();

            messageTeacher.Invoke(group.ToUpper(), "", chatId);

            Assert.True(messageTeacher.IsSended);
        }

        [Theory]
        [InlineData("GБельчич")]
        [InlineData("пары Бельчич")]
        public void Execute_PrivateMessage_ReturnIsSendFalse(string group)
        {
            long chatId = 399418047;
            var messageTeacher = new MessageTeacher();

            messageTeacher.Invoke(group.ToUpper(), "", chatId);

            Assert.False(messageTeacher.IsSended);
        }
    }
}
