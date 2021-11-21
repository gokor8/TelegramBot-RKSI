using RKSI_bot.Databases;
using RKSI_bot.ReservingObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace RKSI_bot.Comand_Message.Commands_Objects
{
    class AdminComand : ICommand
    {
        public List<string> ChatPermissions { get; set; }
        public string[] Triggers { get; set; }

        public void Execute(ref MessageEventArgs message)
        {
            if (message.Message.Chat.Id == 399418047)
            {
                new ScheduleDB(new Databases.PathDB.LocalPathDB("Database")).SendScheduleFromDB("id_person").Wait();
            }
        }
    }
}
