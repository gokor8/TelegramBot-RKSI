using HtmlAgilityPack;
using RKSI_bot.Web.Https;
using RKSI_bot.Web.Parsing;
using RKSI_bot.Web.Parsing.BuildingMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RKSI_bot.Web
{
    public class ParserGroups : AbstractBuildMessage , IParser
    {
        private HtmlDocument htmlDocument;

        public ParserGroups()
        {
            htmlDocument = new HtmlDocument();
        }

        public List<string> GetParsedList(string html)
        {
            htmlDocument.LoadHtml(html);

            var GroupNodes = htmlDocument.DocumentNode.SelectSingleNode("//select[@name='group']").SelectNodes(".//option");

            var Groups = new List<string>();

            foreach (var group in GroupNodes)
            {
                Groups.Add(group.InnerText);
            }

            return Groups;
        }

        public string GetSchedule(string html, bool IsAllSchedule)
        {
            int day = 0;

            htmlDocument.LoadHtml(html);

            if (htmlDocument.DocumentNode.SelectSingleNode($"//b[contains(text(),'{DateTime.Today.AddDays(day).Day}')]") == null)
            {
                if (htmlDocument.DocumentNode.SelectSingleNode($"//b[contains(text(),'{DateTime.Today.AddDays(day + 1).Day}')]") == null)
                    return "Нету расписания на эту группу";
                else
                    day += 1;
            }

            DateTime date = DateTime.Today.AddDays(day).Date;
            var h3Node = htmlDocument.DocumentNode.SelectSingleNode($"//b[contains(text(),'{date.Day}')]");

            string[] nodes = new string[2] { "<b>", "</b>" };
            string message = "";

            foreach (var node in h3Node.SelectNodes(".//following-sibling::*"))
            {
                if (node.Name.ToLower() == "hr")
                {
                    if (IsAllSchedule)
                       message += "--------------------------\r\n";
                    else
                        break;
                }
                else if (node.InnerHtml == "")
                    break;
                else
                    message += nodes[0] + node.InnerHtml.Replace("<br>", "\r\n").Replace("<b>", "").Replace("</br>", "\r\n").Replace("</b>", "") + nodes[1] + "\r\n\r\n";
            }

            string firstMessage = $"<b>Пары на {TranslateDayOfWeek(date.DayOfWeek)}({date.Date.ToString("dd-MM-yyyy")}):\r\n{"=========================="}\r\n</b>";

            return firstMessage + message;
        }
    }
}
