using Newtonsoft.Json;
using RKSI_bot.TelegramElements;
using RKSI_bot.Web;
using RKSI_bot.Web.Https;
using RKSI_bot.Web.Parsing;
using RKSI_bot.WindowsInteractions;
using System;
using System.Collections.Generic;
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

            try
            {
                var excelGroups = HttpRKSI.GetRecentDataArray(new ParserGroups());
                new ExcelGroups(@"C:\Users\Григорий\Desktop\Groups.xlsx").SetDataExcel(excelGroups);
                var excelTeachers = HttpRKSI.GetRecentDataArray(new ParserTeachers());
                new ExcelGroups(@"C:\Users\Григорий\Desktop\Groups.xlsx").SetDataExcel(excelGroups);
            }
            catch (Exception)
            { }

            new AutoRunWindows().SetToAutoRun();

            new TimerSchedule();

            await new TelegramBot().StartBot();

            Console.ReadLine();
        }
    }
}