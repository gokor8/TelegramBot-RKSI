using RKSI_bot.Comand_Message;
using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.Comand_Message.Objects.Commands_Group_Objects;
using RKSI_bot.Commands.Commands_Objects;
using RKSI_bot.TelegramBotClasses.MessageComands;
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
        private CommandsContainer handlerContainer;
        public List<long> SpamIds { get; private set; }

        public CommandsHandler()
        {
            SpamIds = new List<long>();
            handlerContainer = new CommandsContainer();
            SetCommands();
        }

        public async Task Invoke(MessageEventArgs chatInformation)
        {
            string message = chatInformation.Message.Text;
            long chatId = chatInformation.Message.Chat.Id;
            string chatType = chatInformation.Message.Chat.Type.ToString().ToLower();
            /* Ищу в List<Command_link> нужный Command_Link
             * в котором массив triggers имеет значение полученного string trigger */

            var command = handlerContainer.categorysCommands.FirstOrDefault(cmd => cmd.HasTrigger(message, chatType))?.ReturnCommand();

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
                SpamCommand spamCommand = new SpamCommand(SpamIds);
                if (SpamIds.Contains(chatId) && spamCommand.HasChatType(chatType))
                {
                    await spamCommand.Subscribe(message, chatId);
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

        private void SetCommands()
        {
            CommandsChat commandForChat = new CommandsChat();
            CommandsChannel commandForChannel = new CommandsChannel();

            commandForChat.RegisterCommand(new Me(), new List<string> { "group", "supergroup", "private" }, "/me", "🎃");
            commandForChat.RegisterCommand(new List(), new List<string> { "group", "supergroup", "private" }, "/list", "🕴");
            commandForChat.RegisterCommand(new Start(), new List<string> { "group", "supergroup", "private" }, "/start", "/help", "🦾");
            commandForChat.RegisterCommand(new Group(SpamIds), new List<string> { "group", "supergroup", "private" }, "/group", "🏫");
            commandForChat.RegisterCommand(new AdminComand(), "вайяяя");

            commandForChannel.RegisterCommand(new Message(), new List<string> { "group", "supergroup", "private" }, "пары:");

            handlerContainer.AddToCommands(commandForChat);
            handlerContainer.AddToCommands(commandForChannel);
        }
    }
}