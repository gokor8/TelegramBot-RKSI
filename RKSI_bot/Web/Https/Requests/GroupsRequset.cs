using RKSI_bot.Web.Parsing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RKSI_bot.Web.Https
{
    class GroupsRequset : IScheduleRequests
    {
        public async Task<string> SendRKSI(string currentEncoding)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var endEncoding = Encoding.GetEncoding("windows-1251");
            string groupEncoded = HttpUtility.UrlEncode(currentEncoding, endEncoding);
            byte[] bytes = endEncoding.GetBytes($"group={groupEncoded}&stt=qq");
            var byteArrayContent = new ByteArrayContent(bytes);
            byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            var taskResponse = await HttpRKSI.GetInstace().Client.PostAsync("https://www.rksi.ru/schedule", byteArrayContent);
            return await taskResponse.Content.ReadAsStringAsync();
        }
    }
}
