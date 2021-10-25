using System;
using HtmlAgilityPack;
using RKSI_bot.Web.Https;
using RKSI_bot.Web.Parsing;
using RKSI_bot.Web.Parsing.BuildingMessage;


namespace RKSI_bot.Web.Parsing
{
    public class ParsingTeachers : AbstractBuildMessage, IParsingRKSI
    {
        private IScheduleType scheduleType;
        private HtmlDocument htmlDocument;
        private string html;


        public ParsingTeachers(IScheduleType scheduleType)
        {
            this.scheduleType = scheduleType;
            htmlDocument = new HtmlDocument();
        }

        public IParsingRKSI SetHtml(string message)
        {
            html = scheduleType.SendRKSI(message).Result;

            return this;
        }

        public string[][][] GetRecentDataArray()
        {
            var htmlRKSI = HttpRKSI.Client.GetStringAsync("/schedule").Result;
            htmlDocument.LoadHtml(htmlRKSI);

            var Groups = htmlDocument.DocumentNode.SelectSingleNode("//select[@name='teacher']").SelectNodes(".//option");

            string[][][] excelGroups = new string[1][][];
            excelGroups[0] = new string[2][];
            excelGroups[0][1] = new string[Groups.Count];

            for (int group = 0; group < Groups.Count; group++)
            {
                excelGroups[0][1][group] = Groups[group].InnerText;
            }

            return excelGroups;
        }

        public string GetSchedule(bool IsAllSchedule)
        {
            int day = 0;

            htmlDocument.LoadHtml(html);

            DateTime date = DateTime.Today.AddDays(day).Date;
            var h3Node = htmlDocument.DocumentNode.SelectSingleNode($"//h3[contains(text(),'{"."}')]");

            string[] nodes = new string[2] { "<b>", "</b>" };
            string message = "";

            foreach (var node in h3Node.SelectNodes(".//following-sibling::*"))
            {
                if (node.Name.ToLower() == "hr")
                {
                    message += "--------------------------\r\n";
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
