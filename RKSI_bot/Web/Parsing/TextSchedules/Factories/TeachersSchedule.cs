using RKSI_bot.Web.Https;
using RKSI_bot.Web.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.Web.Parsing
{
    public class TeachersSchedule : Schedule
    {
        public override IParser GetParser()
        {
            return new ParserTeachers();
        }

        public override IScheduleRequests GetSheduleRequests()
        {
            return new TeachersRequest();
        }
    }
}
