﻿using RKSI_bot.Web.Https;
using RKSI_bot.Web.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.Web
{
    class GroupsFactory : Schedule
    {
        public override IParser GetParser()
        {
            return new ParserGroups();
        }

        public override IScheduleRequests GetSheduleRequests()
        {
            return new GroupsRequset();
        }
    }
}
