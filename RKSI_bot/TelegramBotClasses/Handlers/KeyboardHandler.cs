using RKSI_bot.Web;
using RKSI_bot.Web.Https;
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
        private ConcurrentDictionary<long, int> editMessages;

        public KeyboardHandler(ConcurrentDictionary<long, int> editMessages, params string[] triggers)
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
                    await HttpRKSI.SendScheduleMessage(message, chatId, new GroupsFactory(), true);
                };
            }).Start();
        }

        private async Task ButtonsLogic(long chatId, int cours)
        {
            var gorupList = new FormationGroup().GetCoursGroups(cours);
            var keyboard = new TelegramMessageKeyboard().GetKeyboard(gorupList, 3);

            if (editMessages.Keys.Contains(chatId))
            {
                try
                {   //Берем из словаря Ид сообщения и редачим сообщение.
                    int editMessageId;
                    editMessages.TryGetValue(chatId, out editMessageId);

                    await TelegramBot.Bot?.EditMessageTextAsync(chatId, editMessageId, "Выберите свою группу", replyMarkup: keyboard);
                }
                catch (Exception)
                { /*Телеграм дает ошибку если, я редактирую сообщение на точно такое же*/ }
            }
            else
            {   //Добавляем в словарь ид чата и ид сообщение, чтобы потом отредачить его
                var messageInfo = await TelegramBot.Bot.SendTextMessageAsync(chatId, "Выберите свою группу", replyMarkup: keyboard);
                editMessages.TryAdd(chatId, messageInfo.MessageId);
            }
        }

    }
}
