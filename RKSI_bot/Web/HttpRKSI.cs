using RKSI_bot.Web.Parsing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RKSI_bot.Web
{
    internal static class HttpRKSI
    {
        public static HttpClient Client;
        private static HttpClientHandler handler;
        private static CookieContainer cookieContainer;

        static HttpRKSI()
        {
            cookieContainer = new CookieContainer();
            handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            Client = new HttpClient(handler) { BaseAddress = new System.Uri("https://www.rksi.ru/") };

            SetDefaultHeaders();
        }

        public static async Task SendScheduleMessage(string message, long chatId, IParsingRKSI typeShedule, bool isAllSchedule = false)
        {
            string html = await SendRequestToRKSI(message);
            string textForMessage = typeShedule.GetSchedule(html, isAllSchedule);

            try
            {
                await TelegramBot.Bot.SendTextMessageAsync(chatId, textForMessage, Telegram.Bot.Types.Enums.ParseMode.Html);
            }catch(System.Exception exc)
            {
                System.Console.WriteLine(exc);
            }
            // Создаю класс, или в классе Parsing завожу нужные переменные, например: chatId, message, и в логику даю из класса эти значения
        }

        private static async Task<string> SendRequestToRKSI(string currentEncoding)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var endEncoding = Encoding.GetEncoding("windows-1251");
            string groupEncoded = HttpUtility.UrlEncode(currentEncoding, endEncoding);
            byte[] bytes = endEncoding.GetBytes($"group={groupEncoded}&stt=qq");
            var byteArrayContent = new ByteArrayContent(bytes);
            byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            var taskResponse = await Client.PostAsync("https://www.rksi.ru/schedule", byteArrayContent);
            return await taskResponse.Content.ReadAsStringAsync();
        }

        public static void SetDefaultHeaders()
        {
            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36");
            Client.DefaultRequestHeaders.Add("Referer", "https://www.rksi.ru/schedule");
            Client.DefaultRequestHeaders.Add("Origin", "https://www.rksi.ru");
            Client.DefaultRequestHeaders.Add("Host", "www.rksi.ru");
            Client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}