using RKSI_bot.ReservingObjects;
using Telegram.Bot.Types.Enums;

namespace RKSI_bot.TelegramBotClasses.Messages
{
    public abstract class ChatFactory
    {
        protected ICommand[] _commands { get; set; }

        public ChatType ChatType;

        public abstract ICommand FindCommand(Telegram.Bot.Types.Message messageInfo);
    }
}
