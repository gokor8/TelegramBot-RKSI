using RKSI_bot.ReservingObjects;
using System;
using System.Collections.Generic;
using Telegram.Bot.Args;

namespace RKSI_bot.Comand_Message
{
    class CommandsChannel : ICommands
    {
        private List<ICommand> Commands;
        private ICommand FoundCommand;

        public CommandsChannel()
        {
            Commands = new List<ICommand>();
        }

        public void RegisterCommand(ICommand command, List<string> chatpermissions, params string[] triggers)
        {
            Commands.Add(command);

            command.ChatPermissions = chatpermissions;
            command.Triggers = triggers;
        }
        public void RegisterCommand(ICommand command, params string[] triggers)
        {
            Commands.Add(command);

            command.ChatPermissions = new List<string> { "private" };
            command.Triggers = triggers;
        }


        public bool HasTrigger(string message, string chatType)
        {
            var foundCommands = Commands.FindAll(x => x.ChatPermissions.Contains(chatType));
            foreach (var command in foundCommands)
                foreach (var findTrgger in command.Triggers)
                {
                    if (message.Contains(findTrgger) && message.Length < 20)
                    {
                        FoundCommand = command;
                        return true;
                    }
                }

            return false;
        }


        public ICommand ReturnCommand()
        {
            return FoundCommand;
        }
    }
}
