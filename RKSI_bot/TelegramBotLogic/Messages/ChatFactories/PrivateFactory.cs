using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.Commands.Commands_Objects;
using RKSI_bot.ReservingObjects;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace RKSI_bot.TelegramBotClasses.Messages.ChatFactories
{
    public class PrivateFactory : ChatFactory
    {
        public PrivateFactory()
        {
            ChatType = ChatType.Private;

            _commands = new ICommand[]
            {
                new Me("/me", "🎃"),
                new List("/list", "🕴"),
                new Start("/start", "/help", "🦾"),
                new Group("/group", "🏫"),
                new Admin("вайяяя")
            };
        }

        public override ICommand FindCommand(string message)
        {
            foreach (var command in _commands)
            {
                foreach (var foundTrigger in command.Triggers)
                {
                    if (message.Equals(foundTrigger))
                    {
                        return command;
                    }
                    else if (message.Contains(foundTrigger) && !message.Contains("/"))
                    {
                        return command;
                    }
                }
            }

            return null;
        }
    }
}
