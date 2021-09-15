using RKSI_bot.TelegramBotClasses;
using RKSI_bot.TelegramBotClasses.Keyboards;
using RKSI_bot.TelegramElements;
using RKSI_bot.Web;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace RKSI_bot
{
    internal class TelegramBot
    {
        public static TelegramBotClient Bot;
        private MassegesHandler messageHandler;
        private KeyboardHandler keyboardHandler;

        private Dictionary<long, int> willEditMessages = new Dictionary<long, int>();

        public TelegramBot()
        {
            Bot = new TelegramBotClient("1283170424:AAHZTtV0Rehsc_MlEBZKKXJLaU4udCbsYd8");
            keyboardHandler = new KeyboardHandler(willEditMessages);
            messageHandler = new MassegesHandler();
        }

        public async Task StartBot()
        {
            setUsersButtons();

            await Bot.SetWebhookAsync("https://api.telegram.org/bot1283170424:AAHZTtV0Rehsc_MlEBZKKXJLaU4udCbsYd8/setWebhook?url=https://SSRtttteeessa:8443/1283170424:AAHZTtV0Rehsc_MlEBZKKXJLaU4udCbsYd8/");

            Bot.OnMessage += (sender, MessageArgs) =>
            {
                willEditMessages.Remove(MessageArgs.Message.Chat.Id);
                messageHandler.OnMessage(MessageArgs);
            };

            Bot.OnCallbackQuery += (sender,CallbackData)
                => keyboardHandler.OnCallbackKeyboard(CallbackData.CallbackQuery.Data, CallbackData.CallbackQuery.Message.Chat.Id);

            Bot.StartReceiving();
        }
        private void setUsersButtons()
        {
            TelegramUserKeyboard userKeyboard = new TelegramUserKeyboard();

            userKeyboard.AddButton("🦾 Помощь");
            userKeyboard.AddButton("👩‍🏫 Рассылка");
            userKeyboard.AddButton("🎃 Моя группа в рассылке");
            userKeyboard.AddButton("🕴 Список групп");
        }
    }
}