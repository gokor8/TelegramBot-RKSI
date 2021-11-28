using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.Commands.Commands_Objects;
using RKSI_bot.ReservingObjects;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace RKSI_bot.TelegramBotClasses.Messages.ChatFactories
{
    public class GroupFactory : ChatFactory
    {
        public GroupFactory()
        {
            ChatType = ChatType.Group;

            _commands = new ICommand[]
            {

            };
        }

        public override ICommand FindCommand(string message)
        {
            foreach (var command in _commands)
            {
                foreach (var findTrgger in command.Triggers)
                {
                    if (message.Contains(findTrgger) && message.Length < 20)
                    {
                        return command;
                    }
                }
            }
            return null;
        }
    }
}
