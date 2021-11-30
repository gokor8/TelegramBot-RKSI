using RKSI_bot.Databases.EntityDataBase;
using RKSI_bot.Web;
using RKSI_bot.Web.Parsing;
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

            new AutoRunWindows().SetToAutoRun();

            new TimerSchedule();

            await new TelegramBot().StartBot();

            Console.ReadLine();
        }
    }
}