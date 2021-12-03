using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramBotClasses.Messages.Objects.CommandsChannel.NudeMessage;

namespace RKSI_bot.Comand_Message.Objects.Commands_Group_Objects
{
    public class Message : ICommand
    {
        public string[] Triggers { get; set; }

        public bool IsExecuted { get; private set; }

        public Message(params string[] triggers)
        {
            Triggers = triggers;
        }

        public void Execute(Telegram.Bot.Types.Message messageInfo)
        {
            string message = messageInfo.Text.Replace(" ","").ToUpper();
            long chatId = messageInfo.Chat.Id;

            var messageSender = new MessageFactory()?.CreateMessage(message);

            if (messageSender != null)
            {
                messageSender.Invoke(message, chatId);
                IsExecuted = true;
            }
        }
    }
}