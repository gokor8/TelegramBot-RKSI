using RKSI_bot.Groups;
using RKSI_bot.Web;
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
            DateTime scheduledTime = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 16, 00, 0, 0); //Specify your scheduled time HH,MM,SS [8am and 42 minutes]
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

            var excelGroups = new ParsingRKSI().GetRecentsGroups().Result;
            new ExcelGroups(@"D:\Users\gzaly\OneDrive\Рабочий стол\Groups.xlsx").SetDataExcel(excelGroups);

            _ = new DataBase().ScheduleDataBase("id_person");

            SchedulingTimer();
        }
    }
}