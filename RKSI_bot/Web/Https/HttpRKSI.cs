using RKSI_bot.Web.Https;
using RKSI_bot.Web.Parsing;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RKSI_bot.Web
{
    public class HttpRKSI
    {
        private static readonly HttpRKSI httpRKSI = new HttpRKSI();

        public HttpClient Client;
        private HttpClientHandler handler;
        public CookieContainer cookieContainer;

        private HttpRKSI()
        {
            cookieContainer = new CookieContainer();
            handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            Client = new HttpClient(handler) { BaseAddress = new System.Uri("https://www.rksi.ru/") };

            SetDefaultHeaders();
        }
        private void SetDefaultHeaders()
        {
            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36");
            Client.DefaultRequestHeaders.Add("Referer", "https://www.rksi.ru/schedule");
            Client.DefaultRequestHeaders.Add("Origin", "https://www.rksi.ru");
            Client.DefaultRequestHeaders.Add("Host", "www.rksi.ru");
            Client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public async Task SendScheduleMessage(string message, long chatId, Schedule typeShedule, bool isAllSchedule = false)
        {
            string textForMessage = await typeShedule.GetParsedText(message, isAllSchedule);
            
            try
            {
                await TelegramBot.Bot.SendTextMessageAsync(chatId, textForMessage, Telegram.Bot.Types.Enums.ParseMode.Html);
            }
            catch (System.Exception exc)
            {
                System.Console.WriteLine(exc);
            }
            // Создаю класс, или в классе Parsing завожу нужные переменные, например: chatId, message, и в логику даю из класса эти значения
        }

        public List<string> GetRecentDataArray(IParser parser)
        {
            var htmlSchedule = Client.GetStringAsync("/schedule").Result;

            var collageUnits = parser.GetParsedList(htmlSchedule);

            return collageUnits;
        }

        public static HttpRKSI GetInstace()
        {
            return httpRKSI;
        }
    }
}