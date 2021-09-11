using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.Web.Parsing.BuildingMessage
{
    abstract class AbstractBuildMessage
    {
        public virtual string TranslateDayOfWeek(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday
                    :
                    return "Понедельник";
                case DayOfWeek.Tuesday
                    :
                    return "Вторник";
                case DayOfWeek.Wednesday
                    :
                    return "Среда";
                case DayOfWeek.Thursday
                    :
                    return "Четверг";
                case DayOfWeek.Friday
                    :
                    return "Пятницу";
                case DayOfWeek.Saturday
                    :
                    return "Субботу";
                case DayOfWeek.Sunday
                    :
                    return "Воскресенье";
            }
            return "";
        }
        public virtual string GetTextLine(string symbol, string message)
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
