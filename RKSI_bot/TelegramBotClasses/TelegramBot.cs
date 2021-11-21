using RKSI_bot.Logs;
using RKSI_bot.TelegramBotClasses.Keyboards;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RKSI_bot
{
    public class TelegramBot
    {
        public static TelegramBotClient Bot;
        public const string TelegramCode = "1283170424:AAHZTtV0Rehsc_MlEBZKKXJLaU4udCbsYd8";

        private MassegesHandler messageHandler;
        private KeyboardHandler keyboardHandler;
        private Log log;

        private ConcurrentDictionary<long, int> willEditMessages = new ConcurrentDictionary<long, int>();

        public TelegramBot()
        {
            Bot = new TelegramBotClient(TelegramCode);
            keyboardHandler = new KeyboardHandler(willEditMessages);
            messageHandler = new MassegesHandler();
            log = new LogConsole();
        }

        public async Task StartBot()
        {
            await Bot.SetWebhookAsync($"https://api.telegram.org/bot{TelegramCode}/setWebhook?url=https://SSRtttteeessa:8443/{TelegramCode}/");

            Bot.OnMessage += (sender, MessageArgs) =>
            {
                log.SetLog(MessageArgs);

                int value;
                willEditMessages.TryRemove(MessageArgs.Message.Chat.Id, out value);

                messageHandler.OnMessage(MessageArgs);
            };

            Bot.OnCallbackQuery += (sender, CallbackData) =>
            {
                log.SetLog(CallbackData);

                keyboardHandler.OnCallbackKeyboard(CallbackData.CallbackQuery.Data, CallbackData.CallbackQuery.Message.Chat.Id);
            };

            Bot.StartReceiving();
        }
    }
}