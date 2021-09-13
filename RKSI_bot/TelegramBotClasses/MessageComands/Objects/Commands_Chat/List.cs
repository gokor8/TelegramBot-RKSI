using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace RKSI_bot.ReservingObjects
{
    class List : ICommand
    {
        public List<string> ChatPermissions { get; set; }
        public string[] Triggers { get; set; }

        private TelegramMessageKeyboard telegramKeyboard;
        public List()
        {
            telegramKeyboard = new TelegramMessageKeyboard();
        }
        public void Execute(ref MessageEventArgs message)
        {
            var keyboard = telegramKeyboard.GetStartGroups();
            TelegramBot.Bot.SendTextMessageAsync(message.Message.Chat.Id, "-----------\r\n|РКСИ|\r\n-----------", replyMarkup: keyboard);
        }
    }
}
