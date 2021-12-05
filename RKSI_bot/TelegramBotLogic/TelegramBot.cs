using RKSI_bot.Logs;
using RKSI_bot.TelegramBotClasses.Keyboards;
using RKSI_bot.WindowsInteractions;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RKSI_bot
{
    public class TelegramBot
    {
        public static TelegramBotClient Bot { get; private set; }
        public readonly string TelegramCode = new FileTelegramApi().GetApiKey();

        private MassegesHandler messageHandler;
        private KeyboardHandler keyboardHandler;
        private Log _log = new LogConsole();
        private Log _fileLog = new LogConsole();

        private ConcurrentDictionary<long, int> willEditMessages = new ConcurrentDictionary<long, int>();

        public TelegramBot()
        {
            Bot = new TelegramBotClient(TelegramCode);
            keyboardHandler = new KeyboardHandler(willEditMessages);
            messageHandler = new MassegesHandler();
        }

        public async Task StartBot()
        {
            await Bot.SetWebhookAsync($"https://api.telegram.org/bot{TelegramCode}/setWebhook?url=https://SSRtttteeessa:8443/{TelegramCode}/");

            Bot.OnMessage += (sender, messageArgs) =>
            {
                var messageInfo = messageArgs.Message;

                _log.SetLog(messageInfo);
                _fileLog.SetLog(messageInfo);

                int value;
                willEditMessages.TryRemove(messageInfo.Chat.Id, out value);

                messageHandler.OnMessage(messageInfo);
            };

            Bot.OnCallbackQuery += (sender, CallbackData) =>
            {
                _log.SetLog(CallbackData);
                _fileLog.SetLog(CallbackData);

                keyboardHandler.OnCallbackKeyboard(CallbackData.CallbackQuery.Data, CallbackData.CallbackQuery.Message.Chat.Id);
            };

            Bot.StartReceiving();
        }
    }
}