using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace RKSI_bot.ReservingObjects
{
    interface ICommand
    {
        public List<string> ChatPermissions { get; set; }
        public string [] Triggers { get; set; }

        public abstract void Execute(ref MessageEventArgs message);
    }
}
