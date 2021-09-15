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
        private List<string> Courses;

        public TelegramMessageKeyboard()
        {
            Courses = new List<string>();
        }
        public InlineKeyboardMarkup GetStartGroups()
        {
            List<InlineKeyboardButton[]> listArrayButtons = new List<InlineKeyboardButton[]>();

            List<InlineKeyboardButton> listButtons = new List<InlineKeyboardButton>();

            listArrayButtons.Add(new[] { 
                    InlineKeyboardButton.WithCallbackData("1 курс", "1"),
                    InlineKeyboardButton.WithCallbackData("2 курс", "2"),
                    InlineKeyboardButton.WithCallbackData("3 курс", "3"),
                    InlineKeyboardButton.WithCallbackData("4 курс", "4")
                    });

            return new InlineKeyboardMarkup(listArrayButtons);
        }

        private void SetCourses(int cours)
        {
            foreach (var group in GroupsContainer.Groups)
            {
                string cleanCours = Regex.Replace(group, @"[^0-9]", "");

                if (Convert.ToInt32(cleanCours[0].ToString()) == cours)
                {
                    Courses.Add(group);
                }
            }
        }

        public InlineKeyboardMarkup GetKeyboard(int countCours, int sizeKeyboard)
        {
            SetCourses(countCours);

            /* Проверяет базу групп, и добавляет в List группы нажатаго курса (1,2,3,4)
            * Далее, он раскидывает эти группы на кнопки(разметку)*/
            //Console.WriteLine(Courses.Count() + " направлений");
            List<InlineKeyboardButton[]> keyboard = new List<InlineKeyboardButton[]>();

            int countCourses = Courses.Count();
            for (int specialization = 0; specialization < countCourses;)
            {
                int evennessRow = specialization + sizeKeyboard;

                if (evennessRow > countCourses)
                {
                    sizeKeyboard = countCourses - specialization;
                }

                InlineKeyboardButton[] longKeyboard = new InlineKeyboardButton[sizeKeyboard];

                for (int i = 0; i < sizeKeyboard; i++)
                {
                    longKeyboard[i] = InlineKeyboardButton.WithCallbackData(Courses[specialization + i], Courses[specialization + i]);
                }

                specialization += sizeKeyboard;

                keyboard.Add(longKeyboard);
            }
            Courses.Clear();

            return new InlineKeyboardMarkup(keyboard);
        }
    }
}
