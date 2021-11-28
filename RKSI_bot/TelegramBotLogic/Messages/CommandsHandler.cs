using RKSI_bot.Comand_Message;
using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.Comand_Message.Objects.Commands_Group_Objects;
using RKSI_bot.Commands.Commands_Objects;
using RKSI_bot.TelegramBotClasses.Messages.ChatFactories;
using RKSI_bot.TelegramBotLogic.Messages;
using RKSI_bot.TelegramElements;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace RKSI_bot.TelegramBotClasses.Messages
{
    internal class CommandsHandler
    {
        private List<ChatFactory> _factoriesContainer;
        private List<long> SpamIds = SpamDataStore.GetInstace().MessageIds;

        public CommandsHandler()
        {
            _factoriesContainer = new List<ChatFactory>() { new GroupFactory(), new PrivateFactory() };
        }

        public async Task Invoke(MessageEventArgs chatInformation)
        {
            SpamCommand spamCommand = new SpamCommand();

            string message = chatInformation.Message.Text.Replace("b","").Replace("w","");
            long chatId = chatInformation.Message.Chat.Id;
            ChatType chatType = chatInformation.Message.Chat.Type;
            
            /* Ищу в List<Command_link> нужный Command_Link
             * в котором массив triggers имеет значение полученного string trigger */
            var command = _factoriesContainer.FirstOrDefault(cmd => cmd.ChatType == chatType)?.FindCommand(message);

            if (command != null)
            {
                if ((SpamIds?.Count ?? 0) != 0)
                    SpamIds.Remove(chatId);
                // Если триггер найден в каком - либо ICommands
                // выполняем метод в классе, который унаследован от интерфейса

                command.Execute(chatInformation);
            }
            else
            {
                if (SpamIds.Contains(chatId) && spamCommand.chatTypes.FirstOrDefault(c=>c == chatType)== chatType)
                {
                    await spamCommand.Subscribe(message, chatId);
                }
                else if (chatType is ChatType.Private)
                {
                    new Message().Execute(chatInformation);
                }
            }
        }
    }
}