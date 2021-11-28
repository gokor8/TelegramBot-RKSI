using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace RKSI_bot.ReservingObjects
{
    class List : ICommand
    {
        public string[] Triggers { get; set; }

        public List(params string[] triggers)
        {
            Triggers = triggers;
        }

        public void Execute(MessageEventArgs messageInfo)
        {
            Dictionary<string, string> courses = new Dictionary<string, string>() { { "1 курс", "1" }, { "2 курс", "2" }, { "3 курс", "3" }, { "4 курс", "4" } };
            var keyboard = new TelegramMessageKeyboard().GetKeyboard(courses, courses.Count);

            TelegramBot.Bot.SendTextMessageAsync(messageInfo.Message.Chat.Id, "-----------\r\n|РКСИ|\r\n-----------", replyMarkup: keyboard);
        }
    }
}
