using RKSI_bot.Comand_Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.TelegramBotClasses.MessageComands
{
    class CommandsContainer
    {
        public List<ICommands> categorysCommands;

        public CommandsContainer()
        {
            categorysCommands = new List<ICommands>();
        }
        public void AddToCommands(ICommands command)
            => categorysCommands.Add(command);
    }
}
