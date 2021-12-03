using RKSI_bot.Databases;
using RKSI_bot.ReservingObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace RKSI_bot.Comand_Message.Commands_Objects
{
    class Admin : ICommand
    {
        public string[] Triggers { get; set; }
        public bool IsExecuted { get; private set; }
        public Admin(params string[] triggers)
        {
            Triggers = triggers;
        }

        public void Execute(Telegram.Bot.Types.Message messageInfo)
        {
            if (messageInfo.Chat.Id == 399418047)
            {
                new SpamScheduleDataBase(new Databases.PathDB.LocalPathDB("Database")).SendScheduleFromDB("ChatId").Wait();

                IsExecuted = true;
            }
        }
    }
}
