using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramBotClasses.Messages.Objects.CommandsChannel.NudeMessage;

namespace RKSI_bot.Comand_Message.Objects.Commands_Group_Objects
{
    public class Message : ICommand
    {
        public string[] Triggers { get; set; }

        private string _keyWord = "";
        public bool IsExecuted { get; set; }

        public Message(params string[] triggers)
        {
            Triggers = triggers;
        }

        public Message SetKeyWord(string keyWord)
        {
            _keyWord = keyWord;

            return this;
        }

        public void Execute(Telegram.Bot.Types.Message messageInfo)
        {
            string message = messageInfo.Text.Replace(" ","").ToUpper();
            long chatId = messageInfo.Chat.Id;

            var messageSender = new MessageFactory()?.CreateMessage(message);

            if (messageSender != null)
            {
                messageSender.Invoke(message, _keyWord, chatId);
                IsExecuted = true;
            }
        }
    }
}