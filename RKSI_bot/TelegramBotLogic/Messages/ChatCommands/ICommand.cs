using System.Collections.Generic;
using Telegram.Bot.Args;

namespace RKSI_bot.ReservingObjects
{
    public interface ICommand
    {
        bool IsExecuted { get; }
        string [] Triggers { get; set; }

        abstract void Execute(Telegram.Bot.Types.Message messageInfo);
    }
}
