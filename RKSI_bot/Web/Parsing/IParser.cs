using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RKSI_bot.Web.Parsing
{
    public interface IParser
    {
        string GetSchedule(string html, bool IsAllSchedule);

        string[][][] GetParsedList(string html);
    }
}
