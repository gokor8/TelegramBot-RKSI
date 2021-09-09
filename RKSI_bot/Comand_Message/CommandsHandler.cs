using RKSI_bot.Comand_Message;
using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.Comand_Message.Objects.Commands_Group_Objects;
using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace RKSI_bot.ReservingObjects
{
    internal class CommandsHandler
    {
        private static List<ICommands> categorysCommands;

        public static List<long> SpamIds = new List<long>();

        public CommandsHandler()
        {
            categorysCommands = new List<ICommands>();
        }

        public void Register(ICommands command)
            => categorysCommands.Add(command);

        public async Task Invoke(MessageEventArgs chatInformation)
        {
            string message = chatInformation.Message.Text;
            long chatId = chatInformation.Message.Chat.Id;
            string chatType = chatInformation.Message.Chat.Type.ToString().ToLower();
            /* Ищу в List<Command_link> нужный Command_Link
             * в котором массив triggers имеет значение полученного string trigger */

            var command = categorysCommands.FirstOrDefault(cmd => cmd.HasTrigger(message, chatType))?.ReturnCommand();

            if (command != null)
            {
                LogAndDeleteUser(in chatInformation);
                // Если триггер найден в каком - либо ICommands
                // выполняем метод в классе, который унаследован от интерфейса
                // var userCommand = command.ReturnCommand();
                command.Execute(ref chatInformation);
            }
            else
            {
                SpamCommand spamCommand = new SpamCommand();

                if (SpamIds.Contains(chatId) && spamCommand.HasChatType(chatType))
                {
                    await spamCommand.SpamRegistration(message, chatId);
                }
                else if (chatType == "private")
                {
                    new Message().Execute(ref chatInformation);
                }
            }
        }

        private void LogAndDeleteUser(in MessageEventArgs message)
        {
            long chatId = message.Message.Chat.Id;

            if ((SpamIds?.Count ?? 0) != 0)
            {
                long userId = SpamIds.FirstOrDefault(usr => usr == chatId);
                SpamIds.Remove(userId);
            }

            Console.WriteLine($"{message.Message.Chat.Username} | {message.Message.Chat.Id} - {message.Message.Chat.FirstName} : {message.Message.Chat.LastName} : {message.Message.Text} -- {DateTime.Now} | Username - {message.Message.Chat.Username}");
        }

        private void NotFound(string trigger)
        {
            Console.WriteLine($"Команда {trigger} не распознана!");
        }
    }
}