using RKSI_bot.Comand_Message;
using System.Collections.Generic;

namespace RKSI_bot.ReservingObjects
{
    class CommandsChat : ICommands
    {
        private List<ICommand> Commands;
        private ICommand FoundCommand;

        public CommandsChat()
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

        public bool HasTrigger(string trigger, string chatType = "private")
        {
            var chatTypeCommands = Commands.FindAll(x => x.ChatPermissions.Contains(chatType));

            foreach (var command in chatTypeCommands)
                foreach (var findTrgger in command.Triggers)
                {
                    if (trigger.Equals(findTrgger))
                    {
                        FoundCommand = command;
                        return true;
                    }
                    else if (trigger.Contains(findTrgger) && !findTrgger.Contains("/"))
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
