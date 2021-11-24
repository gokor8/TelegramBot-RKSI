using Newtonsoft.Json;
using RKSI_bot.Databases.EntityDataBase;
using RKSI_bot.TelegramElements;
using RKSI_bot.Web;
using RKSI_bot.Web.Parsing;
using RKSI_bot.WindowsInteractions;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RKSI_bot
{
    internal class Program
    {
        // Каждый учебный год обновлять в бд
        private static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            HttpRKSI httpRKSI = HttpRKSI.GetInstace();


            using (var context = new CollageUnitsDb())
            {

                var Groups = httpRKSI.GetRecentDataArray(new ParserGroups());
                var Teachers = httpRKSI.GetRecentDataArray(new ParserTeachers());

            }

            new AutoRunWindows().SetToAutoRun();

            new TimerSchedule();

            await new TelegramBot().StartBot();

            Console.ReadLine();
        }
    }
}