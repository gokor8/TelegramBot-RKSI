using RKSI_bot.ReservingObjects;
using System.Collections.Generic;

namespace RKSI_bot.Comand_Message
{
    interface ICommands
    {
        public void RegisterCommand(ICommand command, List<string> chatpermissions, params string[] triggers);
        public void RegisterCommand(ICommand command, params string[] triggers);

        bool HasTrigger(string message, string chatType);

        ICommand ReturnCommand();
    }
}
