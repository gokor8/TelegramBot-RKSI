using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramBotClasses.Messages.Objects.CommandsChannel.NudeMessage;
using System.Text.RegularExpressions;
using Telegram.Bot.Args;

namespace RKSI_bot.Comand_Message.Objects.Commands_Group_Objects
{
    internal class Message : ICommand
    {
        public string[] Triggers { get; set; }

        public Message(params string[] triggers)
        {
            Triggers = triggers;
        }

        public void Execute(MessageEventArgs MessageInfo)
        {
            string message = Regex.Replace(MessageInfo.Message.Text, @"\s+", "").Replace("b","").Replace("w","").ToUpper();
            long chatId = MessageInfo.Message.Chat.Id;

            new MessageFactory()?.CreateMessage(message).Invoke(message, chatId);
        }
    }
}