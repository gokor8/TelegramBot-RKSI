
using RKSI_bot.Databases.PathDB;
using RKSI_bot.ReservingObjects;

namespace RKSI_bot.Comand_Message.Commands_Objects
{
    class Me : ICommand
    {
        public string[] Triggers { get; set; }
        public bool IsExecuted { get; protected set; }
        public Me(params string[] triggers)
        {
            Triggers = triggers;
        }

        public virtual void Execute(Telegram.Bot.Types.Message messageInfo)
        {
            long chatId = messageInfo.Chat.Id;
            TelegramBot.Bot.SendTextMessageAsync(chatId, "Подождите, 3 секунды, ищу информацию в базе данных...");

            object faculty = new DataBase(messageInfo.Text, messageInfo.Chat.Id, new LocalPathDb("Database")).ExcecuteCommand($"SELECT Facult FROM UserTable WHERE ChatId = '{chatId}'");

            if (faculty != null)
            {
                TelegramBot.Bot.SendTextMessageAsync(messageInfo.Chat.Id, messageInfo.Chat.FirstName + " " + messageInfo.Chat.LastName + messageInfo.Chat.Title + " : " + faculty.ToString()).Wait();
                IsExecuted = true;
            }
            else
            {
                TelegramBot.Bot.SendTextMessageAsync(messageInfo.Chat.Id, "Ты пока не подписался на рассылку").Wait();
                IsExecuted = false;
            }
        }
    }
}