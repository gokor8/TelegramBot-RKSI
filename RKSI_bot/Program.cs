using Newtonsoft.Json;
using RKSI_bot.Groups;
using RKSI_bot.TelegramElements;
using RKSI_bot.Web;
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

            var excelGroups = await new ParsingRKSI().GetRecentsGroups();
            new ExcelGroups(@"D:\Users\gzaly\OneDrive\Рабочий стол\Groups.xlsx").SetDataExcel(excelGroups);

            new AutoRunWindows().SetToAutoRun();

            new TimerSchedule();

            await new TelegramBot().StartBot();

            Console.ReadLine();
        }

        public static bool CheckDictonary<T, C>(in Dictionary<T, C> dictonary, T key)
        {
            foreach (var value in dictonary)
            {
                if (value.Key.Equals(key))
                {
                    return true;
                }
            }

            return false;
        }

        public static C GetValueDictonary<T, C>(in Dictionary<T, C> dictonary, T key)
        {
            foreach (var value in dictonary)
            {
                if (value.Key.Equals(key))
                {
                    return value.Value;
                }
            }

            return default;
        }
    }
}