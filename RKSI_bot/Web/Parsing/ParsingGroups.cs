using HtmlAgilityPack;
using RKSI_bot.Web.Parsing;
using RKSI_bot.Web.Parsing.BuildingMessage;
using System;

namespace RKSI_bot.Web
{
    class ParsingGroups : AbstractBuildMessage , IParsingRKSI
    {
        private HtmlDocument htmlDocument;

        public ParsingGroups()
        {
            htmlDocument = new HtmlDocument();
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

        public string[][][] GetRecentDataArray()
        {
            var htmlRKSI = HttpRKSI.Client.GetStringAsync("/schedule").Result;
            htmlDocument.LoadHtml(htmlRKSI);

            var Groups = htmlDocument.DocumentNode.SelectSingleNode("//select[@name='group']").SelectNodes(".//option");

            string[][][] excelGroups = new string[1][][];
            excelGroups[0] = new string[1][];
            excelGroups[0][0] = new string[Groups.Count];

            for (int group = 0; group < Groups.Count; group++)
            {
                excelGroups[0][0][group] = Groups[group].InnerText;
               // Console.WriteLine($"Groups[{ group }] = \"{ Groups[group].InnerText }\" ;");
            }

            return excelGroups;
        }
    }
}
