using RKSI_bot.Web;
using System;
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
        private MassegesHandler callbackHandler;

        public TelegramBot()
        {
            Bot = new TelegramBotClient("1283170424:AAHZTtV0Rehsc_MlEBZKKXJLaU4udCbsYd8");
            callbackHandler = new MassegesHandler();
        }

        public async Task StartBot()
        {
            await Bot.SetWebhookAsync("https://api.telegram.org/bot1283170424:AAHZTtV0Rehsc_MlEBZKKXJLaU4udCbsYd8/setWebhook?url=https://SSRtttteeessa:8443/1283170424:AAHZTtV0Rehsc_MlEBZKKXJLaU4udCbsYd8/");

            Bot.OnMessage += callbackHandler.OnMessage;
            Bot.OnCallbackQuery += callbackHandler.OnCallbackQuery;

            Bot.StartReceiving();
        }
    }
}