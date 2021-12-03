using RKSI_bot.Databases.PathDB;
using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramBotClasses.Keyboards.UserKeyboard;
using RKSI_bot.TelegramElements;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace RKSI_bot.Commands.Commands_Objects
{
    class Start : ICommand
    {
        public string[] Triggers { get; set; }
        public bool IsExecuted { get; private set; }
        public Start(params string[] triggers)
        {
            Triggers = triggers;
        }

        public void Execute(Telegram.Bot.Types.Message messageInfo)
        {
            TelegramBot.Bot.SendTextMessageAsync(messageInfo.Chat.Id, "Вот список команд:\n\r" +
            "/help \r\n" +
            "/group - Присылаю твое расписание каждый день \r\n" +
            "/me - Группа, которую ты указал в рассылке расписания\r\n" +
            "Либо просто пришли мне свою группу, и я скину расписание(Пример: чпокс-51)",
               replyMarkup: GetUserKeyobardFormDB(messageInfo.Chat.Id)).Wait();

            IsExecuted = true;
        }

        private ReplyKeyboardMarkup GetUserKeyobardFormDB(long chatId)
        {
            TelegramUserKeyboard userKeyboard = new TelegramUserKeyboard();

            var userGroup = new DataBase(new LocalPathDB("Database")).ExcecuteCommand($"SELECT Facult FROM ButtonFacultTable WHERE ChatId = '{chatId}'");

            if (userGroup != null)
            {
                userKeyboard.AddButton(userGroup.ToString());
            }

            return userKeyboard.GetKeyboard();
        }
    }
}
