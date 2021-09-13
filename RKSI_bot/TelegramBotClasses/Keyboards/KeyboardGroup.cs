using RKSI_bot.Groups;
using RKSI_bot.TelegramBotClasses.MessageHandlers;
using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace RKSI_bot.TelegramBotClasses.Keyboards
{
    class KeyboardGroup : IKeyboardLogic
    {
        private Dictionary<long, int> editMessages;

        public string[] triggers { get; set; }

        public KeyboardGroup(Dictionary<long, int> editMessages, params string[] triggers)
        {
            this.triggers = triggers;
            this.editMessages = editMessages;
        }

        public void Invoke(string message, long chatId)
        {
            new Task(async () =>
            {
                switch (message)
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
                            await HttpRKSI.SendScheduleMessage(message, chatId, new ParsingGroups(), true);
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

            if (valueDictonarys.GetBoolDictonary(in editMessages, chatId))
            {
                try
                {
                    //Берем из словаря Ид сообщения и редачим сообщение.
                    int editMessageId = valueDictonarys.GetDataDictonary(in editMessages, chatId).First().Value;
                    await TelegramBot.Bot?.EditMessageTextAsync(chatId, editMessageId, "Выберите свою группу", replyMarkup: keyboard);
                }
                catch (Exception)
                { /*Телеграм дает ошибку если, я редактирую сообщение на точно такое же*/ }
            }
            else
            {
                //Добавляем в словарь ид чата и ид сообщение, чтобы потом отредачить его
                var addMessageId = await TelegramBot.Bot.SendTextMessageAsync(chatId, "Выберите свою группу", replyMarkup: keyboard);
                editMessages.Add(chatId, addMessageId.MessageId);
            }
        }

        public Dictionary<long, int> GetEditedDictonary()
        {
            return editMessages;
        }

    }
}
