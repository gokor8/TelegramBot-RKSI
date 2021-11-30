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
            SpamSubcribe spamCommand = new SpamSubcribe();

            string message = chatInformation.Message.Text;
            chatInformation.Message.Text = message.Contains("/") ? message : message.Replace("b", "").Replace("w", "");
            message = chatInformation.Message.Text;

            long chatId = chatInformation.Message.Chat.Id;
            ChatType chatType = chatInformation.Message.Chat.Type;
            
            
            var command = _factoriesContainer.FirstOrDefault(cmd => cmd.ChatType == chatType)?.FindCommand(message);

            if (command != null)
            {
                if ((SpamIds?.Count ?? 0) != 0)
                    SpamIds.Remove(chatId);

                // Если триггер найден в каком - либо ICommands, выполняем метод в классе
                command.Execute(chatInformation);
            }
            else if(SpamIds.Contains(chatId))
            {
                await spamCommand.Subscribe(message, chatId);
            }
        }
    }
}