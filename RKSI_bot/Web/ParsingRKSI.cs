//Gokor8
using HtmlAgilityPack;
using RKSI_bot.Groups;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RKSI_bot.Web
{
    class ParsingRKSI
    {
        private HtmlDocument htmlDocument;

        public ParsingRKSI()
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
                       message += GetTextLine("-", message.Replace("<b>", "").Replace("</b>", ""))+"\r\n";
                    else
                        break;
                }
                else
                message += nodes[0] + node.InnerHtml.Replace("<br>", "\r\n").Replace("<b>", "").Replace("</br>", "\r\n").Replace("</b>", "") + nodes[1] + "\r\n\r\n";
            }

            string firstMessage = $"Пары на {TranslateDayOfWeek(date.DayOfWeek)}({date.Day}:{date.Month}:{date.Year}):\r\n{"=======================" + message.Replace("<b>", "").Replace("</b>", "")}\r\n";

            return firstMessage + message;
        }

        public async Task<string[][][]> GetRecentsGroups()
        {
            var htmlRKSI = await HttpRKSI.Client.GetStringAsync("/schedule");
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
        private string TranslateDayOfWeek(DayOfWeek day)
        {
            switch (day) {
                case DayOfWeek.Monday
                    : return "Понедельник";
                case DayOfWeek.Tuesday
                    : return "Вторник";
                case DayOfWeek.Wednesday
                    : return "Среда";
                case DayOfWeek.Thursday
                    : return "Четверг";
                case DayOfWeek.Friday
                    : return "Пятница";
                case DayOfWeek.Saturday
                    : return "Суббота";
                case DayOfWeek.Sunday
                    : return "Воскресенье";
            }
            return "";
        }
        private string GetTextLine(string symbol, string message)
        {
            string line = "";

            foreach (var messageString in message.Split("\n"))
            {
                if (messageString.Length > line.Length && messageString.Length <= 57)
                {
                    int lenght = messageString.Length - line.Length;

                    for (int i = 0; i < lenght; i++)
                    {
                        line += symbol;
                    }
                }
                else if (messageString.Length > 57)
                {
                    int lenght = 57 - line.Length;

                    for (int i = 0; i < lenght; i++)
                    {
                        line += symbol;
                    }
                }
            }

            return line;
        }
    }
}

//Gokor8