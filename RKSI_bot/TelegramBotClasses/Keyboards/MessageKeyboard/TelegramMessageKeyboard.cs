using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace RKSI_bot
{
    class TelegramMessageKeyboard
    {
        public InlineKeyboardMarkup GetKeyboard(List<string> buttonsData, int sizeKeyboard)
        {
            /* Проверяет базу групп, и добавляет в List группы нажатаго курса (1,2,3,4)
            * Далее, он раскидывает эти группы на кнопки(разметку)*/
            //Console.WriteLine(Courses.Count() + " направлений");
            List<InlineKeyboardButton[]> keyboard = new List<InlineKeyboardButton[]>();

            int countCourses = buttonsData.Count;

            for (int yKeyboard = 0; yKeyboard < countCourses;)
            {
                int evennessRow = yKeyboard + sizeKeyboard;

                if (evennessRow > countCourses)
                {
                    sizeKeyboard = countCourses - yKeyboard;
                }

                InlineKeyboardButton[] xKeyboard = new InlineKeyboardButton[sizeKeyboard];

                for (int button = 0; button < sizeKeyboard; button++)
                {
                    xKeyboard[button] = InlineKeyboardButton.WithCallbackData(buttonsData[yKeyboard + button], buttonsData[yKeyboard + button]);
                }

                yKeyboard += sizeKeyboard;

                keyboard.Add(xKeyboard);
            }
            buttonsData.Clear();

            return new InlineKeyboardMarkup(keyboard);
        }

        public InlineKeyboardMarkup GetKeyboard(Dictionary<string,string> buttonsData, int sizeKeyboard)
        {
            List<InlineKeyboardButton[]> keyboard = new List<InlineKeyboardButton[]>();

            int countCourses = buttonsData.Count;

            for (int yKeyboard = 0; yKeyboard < countCourses;)
            {
                int evennessRow = yKeyboard + sizeKeyboard;

                if (evennessRow > countCourses)
                {
                    sizeKeyboard = countCourses - yKeyboard;
                }

                InlineKeyboardButton[] xKeyboard = new InlineKeyboardButton[sizeKeyboard];

                for (int i = 0; i < sizeKeyboard; i++)
                {
                    var buttonObject = buttonsData.ElementAt(yKeyboard + i);
                    xKeyboard[i] = InlineKeyboardButton.WithCallbackData(buttonObject.Key, buttonObject.Value);
                }

                yKeyboard += sizeKeyboard;

                keyboard.Add(xKeyboard);
            }
            buttonsData.Clear();

            return new InlineKeyboardMarkup(keyboard);
        }
    }
}
