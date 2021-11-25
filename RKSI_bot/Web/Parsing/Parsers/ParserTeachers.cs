﻿using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using RKSI_bot.Web.Https;
using RKSI_bot.Web.Parsing;
using RKSI_bot.Web.Parsing.BuildingMessage;


namespace RKSI_bot.Web.Parsing
{
    public class ParserTeachers : AbstractBuildMessage, IParser
    {
        private HtmlDocument htmlDocument;

        public ParserTeachers()
        { 
            htmlDocument = new HtmlDocument();
        }

        public List<string> GetParsedList(string html)
        {
            htmlDocument.LoadHtml(html);

            var TeacherNodes = htmlDocument.DocumentNode.SelectSingleNode("//select[@name='teacher']").SelectNodes(".//option");

            var Teachers = new List<string>();

            foreach (var teacher in TeacherNodes)
            {
                Teachers.Add(teacher.InnerText);
            }

            return Teachers;
        }

        public string GetSchedule(string html, bool IsAllSchedule)
        {
            int day = 0;

            htmlDocument.LoadHtml(html);

            DateTime date = DateTime.Today.AddDays(day).Date;
            var h3Node = htmlDocument.DocumentNode.SelectSingleNode($"//h3[contains(text(),'{"."}')]");

            if (h3Node is null)
                return "Нету расписания";

            string[] nodes = new string[2] { "<b>", "</b>" };
            string message = "";

            //if (h3Node.SelectNodes(".//following-sibling::*")?.FirstOrDefault() is null)
             //   return "Нету расписания";

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