using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RKSI_bot.Web.Parsing
{
    public interface IParsingRKSI
    {
        IParsingRKSI SetHtml(string message);
        string GetSchedule(bool IsAllSchedule);

        string[][][] GetRecentDataArray();
    }
}
