using System.Collections.Generic;
using Telegram.Bot.Args;

namespace RKSI_bot.ReservingObjects
{
    public interface ICommand
    {
        string [] Triggers { get; set; }

        abstract void Execute(MessageEventArgs messageInfo);
    }
}
