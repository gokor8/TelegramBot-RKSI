using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.Comand_Message.Objects.Commands_Group_Objects;
using RKSI_bot.Commands.Commands_Objects;
using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramBotLogic.Messages.ChatCommands.Groups;
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
                new MeGroups("/me", "🎃"),
                new List("/list", "🕴"),
                new Start("/start", "/help", "🦾"),
                new Message("пары "),
                new Group("/group", "🏫"),
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
