using RKSI_bot.Databases;
using RKSI_bot.Databases.PathDB;
using RKSI_bot.Web;
using RKSI_bot.Web.Https;
using RKSI_bot.WindowsInteractions;
using System;
using System.Timers;

namespace RKSI_bot
{
    internal class TimerSchedule
    {
        private Timer Timer;

        public TimerSchedule()
        {
            SchedulingTimer();
        }

        private void SchedulingTimer()
        {
            Console.WriteLine("### Timer Started ###");
            DateTime nowTime = DateTime.Now;
            DateTime scheduledTime = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 16, 00, 0, 0); //HH,MM,SS [8am and 42 minutes]
            if (nowTime > scheduledTime)
            {
                scheduledTime = scheduledTime.AddDays(1);
            }

            double tickTime = (double)(scheduledTime - DateTime.Now).TotalMilliseconds;
            Timer = new Timer(tickTime);
            Timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            Timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("### Timer Stopped ### \n");
            Timer.Stop();

            var excelGroups = HttpRKSI.GetInstace().GetRecentDataArray(new ParserGroups());
            //new ExcelGroups(@"C:\Users\Григорий\Desktop\Groups.xlsx").SetDataExcel(excelGroups);

            _ = new SpamScheduleDataBase(new LocalPathDB("Database")).SendScheduleFromDB("id_person");

            SchedulingTimer();
        }
    }
}