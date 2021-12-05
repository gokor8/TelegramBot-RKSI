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

        public List<string> GetSchedule(string html, bool IsAllSchedule)
        {
            string[] nodes = new string[2] { "<b>", "</b>" };
            int day = 0;

            htmlDocument.LoadHtml(html);

            if (htmlDocument.DocumentNode.SelectSingleNode($"//b[contains(text(),'{DateTime.Today.AddDays(day).Day}')]") == null)
            {
                if (htmlDocument.DocumentNode.SelectSingleNode($"//b[contains(text(),'{DateTime.Today.AddDays(day + 1).Day}')]") == null)
                    return new List<string> { "Нету расписания на эту группу" };
                else
                    day += 1;
            }

            DateTime date = DateTime.Today.AddDays(day).Date;
            string firstMessage = $"<b>Пары на {TranslateDayOfWeek(date.DayOfWeek)}({date.Date.ToString("dd-MM-yyyy")}):\r\n{"=========================="}\r\n</b>";

            List<string> messageList = new List<string> { firstMessage };


            var h3Node = htmlDocument.DocumentNode.SelectSingleNode($"//b[contains(text(),'{date.Day}')]");
            foreach (var node in h3Node.SelectNodes(".//following-sibling::*"))
            {
                if (node.Name.ToLower() == "hr")
                {
                    if (messageList[messageList.Count - 1].Length > 3870)
                        messageList.Add("");

                    if (IsAllSchedule)
                        messageList[messageList.Count - 1] += "--------------------------\r\n";
                    else
                        break;
                }
                else if (node.InnerHtml == "")
                    break;
                else
                    messageList[messageList.Count - 1] += nodes[0] + ClearHtmlSpaces(node.InnerHtml) + nodes[1] + "\r\n\r\n";
            }

            return messageList;
        }
    }
}
