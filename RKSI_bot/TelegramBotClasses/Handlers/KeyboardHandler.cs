using RKSI_bot.Groups;
using RKSI_bot.Web;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace RKSI_bot.TelegramBotClasses.Keyboards
{
    class KeyboardHandler
    {
        private Dictionary<long, int> editMessages;

        public KeyboardHandler(Dictionary<long, int> editMessages, params string[] triggers)
        {
            this.editMessages = editMessages;
        }

        public void OnCallbackKeyboard(string message, long chatId)
        {
            new Task(async () =>
            {
                try
                {
                    int course = Convert.ToInt32(message);
                    await ButtonsLogic(chatId, course);
                }catch(Exception)
                {
                    await HttpRKSI.SendScheduleMessage(message, chatId, new ParsingGroups(), true);
                };
            }).Start();
        }
        private async Task ButtonsLogic(long chatId, int Cours)
        {
            SearchInArrays valueDictonarys = new SearchInArrays();

            var keyboard = new TelegramMessageKeyboard().GetKeyboard(Cours, 3);

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

    }
}
