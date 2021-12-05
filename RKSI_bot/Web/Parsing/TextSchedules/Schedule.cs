using RKSI_bot.Web.Https;
using RKSI_bot.Web.Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RKSI_bot.Web.Parsing
{
    public abstract class Schedule
    {
        public abstract IParser GetParser();
        public abstract IScheduleRequests GetSheduleRequests();

        public async Task<List<string>> GetParsedText(string message, bool isAllShedule = false)
        {
            var html = await GetSheduleRequests().SendRKSI(message);

            return GetParser().GetSchedule(html, isAllShedule);
        }
    }
}
