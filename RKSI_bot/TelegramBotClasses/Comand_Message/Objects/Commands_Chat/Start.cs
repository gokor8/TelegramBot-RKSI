using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramElements;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace RKSI_bot.Commands.Commands_Objects
{
    class Start : ICommand
    {
        public List<string> ChatPermissions { get; set; }
        public string[] Triggers { get; set; }

        public void Execute(ref MessageEventArgs message)
        {
            var userKeyboard = new TelegramUserKeyboard().GetKeyboard();

            TelegramBot.Bot.SendTextMessageAsync(message.Message.Chat.Id, "Вот список команд:\n\r" +
            "/help \r\n" +
            "/group - Присылаю твое расписание каждый день \r\n" +
            "/me - Группа, которую ты указал в рассылке расписания\r\n" +
            "Либо просто пришли мне свою группу, и я скину расписание(Пример: чпокс-51)",
               replyMarkup: userKeyboard);
        }
    }
}
