using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RKSI_bot.Web.Parsing
{
    interface IParsingRKSI
    {
        string GetSchedule(string html, bool IsAllSchedule);

        string[][][] GetRecentsGroups();
    }
}
