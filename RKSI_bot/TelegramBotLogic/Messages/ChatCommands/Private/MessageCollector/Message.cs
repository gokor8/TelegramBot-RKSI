using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramBotClasses.Messages.Objects.CommandsChannel.NudeMessage;
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

        public void Execute(MessageEventArgs messageInfo)
        {
            string message = messageInfo.Message.Text.Replace(" ","").ToUpper();
            long chatId = messageInfo.Message.Chat.Id;

            var messageSender = new MessageFactory()?.CreateMessage(message);

            if (messageSender != null)
                messageSender.Invoke(message, chatId);
        }
    }
}