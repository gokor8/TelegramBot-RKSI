using RKSI_bot.ReservingObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace RKSI_bot.TelegramBotClasses.Messages
{
    public abstract class ChatFactory
    {
        protected ICommand[] _commands { get; set; }

        public ChatType ChatType;

        public abstract ICommand FindCommand(string message);
    }
}
