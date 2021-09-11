using RKSI_bot.Comand_Message;
using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.Comand_Message.Objects.Commands_Group_Objects;
using RKSI_bot.Commands.Commands_Objects;
using RKSI_bot.Groups;
using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramElements;
using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace RKSI_bot
{
    internal class MassegesHandler
    {
        private CommandsHandler commands;

        private Dictionary<long, int> WillEditMessages = new Dictionary<long, int>();

        public MassegesHandler()
        {
            commands = new CommandsHandler();
            SetCommands();
            SetUsersButtons();
        }

        public void OnMessage(object sender, MessageEventArgs chatInformation)
        {
            new Task(async () =>
            {
                long chatId = chatInformation.Message.Chat.Id;
                WillEditMessages?.Remove(chatId);

                var messageType = chatInformation.Message.Type;
                string message = chatInformation.Message.Text.ToLower().Trim();

                if (messageType != MessageType.Text)
                {
                    string say = RandomSay();
                    await TelegramBot.Bot.SendTextMessageAsync(chatId, say + ". Введите текстовый запрос");
                }
                else
                {
                    await commands.Invoke(chatInformation);
                }

            }).Start();
        }

        public void OnCallbackQuery(object sender, CallbackQueryEventArgs callbackInformation)
        {
            new Task(async () =>
            {
                long chatId = callbackInformation.CallbackQuery.Message.Chat.Id;
                string message = callbackInformation.CallbackQuery.Data;

                switch (callbackInformation.CallbackQuery.Data)
                {
                    case "1 group":
                        await ButtonsLogic(chatId, 1);
                        break;
                    case "2 group":
                        await ButtonsLogic(chatId, 2);
                        break;
                    case "3 group":
                        await ButtonsLogic(chatId, 3);
                        break;
                    case "4 group":
                        await ButtonsLogic(chatId, 4);
                        break;
                    default:
                        if (GroupsContainer.Groups.FirstOrDefault(group => group == message) != null)
                        {
                            await HttpRKSI.SendScheduleMessage(message, chatId, new ParsingGroups() ,true);
                        }
                        break;
                }
            }).Start();
        }
        private async Task ButtonsLogic(long chatId, int Cours)
        {
            TelegramMessageKeyboard telegramkeyboard = new TelegramMessageKeyboard();
            SearchInArrays valueDictonarys = new SearchInArrays();

            var keyboard = telegramkeyboard.GetKeyboard(Cours, 3);

            if (valueDictonarys.GetBoolDictonary(in WillEditMessages, chatId))
            {
                try
                {
                    //Берем из словаря Ид сообщения и редачим сообщение.
                    int editMessageId = valueDictonarys.GetValueDictonary(in WillEditMessages, chatId);
                    await TelegramBot.Bot?.EditMessageTextAsync(chatId, editMessageId, "Выберите свою группу", replyMarkup: keyboard);
                }
                catch (Exception)
                { /*Телеграм дает ошибку если, я редактирую сообщение на точно такое же*/ }
            }
            else
            {
                //Добавляем в словарь ид чата и ид сообщение, чтобы потом отредачить его
                var addMessageId = await TelegramBot.Bot.SendTextMessageAsync(chatId, "Выберите свою группу", replyMarkup: keyboard);
                WillEditMessages.Add(chatId, addMessageId.MessageId);
            }
        }

        private string RandomSay()
        {
            string[] randoms = new string[1];
            randoms[0] = "Безделье-это игрушка дъявола";
            randoms[1] = "Не надо там по углам: курить, шабить";

            return randoms[new Random().Next(0, randoms.Length)];
        }
        private void SetUsersButtons()
        {
            TelegramUserKeyboard userKeyboard = new TelegramUserKeyboard();

            userKeyboard.AddButton("🦾 Помощь");
            userKeyboard.AddButton("👩‍🏫 Рассылка");
            userKeyboard.AddButton("🎃 Моя группа в рассылке");
            userKeyboard.AddButton("🕴 Список групп");
        }
        private void SetCommands()
        {
            CommandsChat commandForChat = new CommandsChat();
            CommandsChannel commandForChannel = new CommandsChannel();

            commandForChat.RegisterCommand(new Me(), new List<string> { "group", "supergroup", "private" }, "/me", "🎃");
            commandForChat.RegisterCommand(new List(), new List<string> { "group", "supergroup", "private" }, "/list", "🕴");
            commandForChat.RegisterCommand(new Start(), new List<string> { "group", "supergroup", "private" }, "/start", "/help", "🦾");
            commandForChat.RegisterCommand(new Group(), new List<string> { "group", "supergroup", "private" }, "/group", "🏫");
            commandForChat.RegisterCommand(new AdminComand(), "вайяяя");

            commandForChannel.RegisterCommand(new Message(), new List<string> { "group", "supergroup", "private" }, "пары:");

            commands.Register(commandForChat);
            commands.Register(commandForChannel);
        }
    }
}