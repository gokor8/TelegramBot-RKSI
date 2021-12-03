using RKSI_bot.Comand_Message.Commands_Objects;
using RKSI_bot.Databases.PathDB;

namespace RKSI_bot.TelegramBotLogic.Messages.ChatCommands.Groups
{
    class MeGroups : Me
    {
        public MeGroups(params string[] triggers) : base(triggers)
        {

        }

        public override void Execute(Telegram.Bot.Types.Message messageInfo)
        {
            long chatId = messageInfo.Chat.Id;
            TelegramBot.Bot.SendTextMessageAsync(chatId, "Подождите, 3 секунды, ищу информацию в базе данных...");

            object faculty = new DataBase(messageInfo.Text, messageInfo.Chat.Id, new LocalPathDB("Database")).ExcecuteCommand($"SELECT Facult FROM UserTable WHERE ChatId = '{chatId}'");

            if (faculty != null)
                TelegramBot.Bot.SendTextMessageAsync(messageInfo.Chat.Id, messageInfo.Chat.Title + " : " + faculty.ToString());
            else
                TelegramBot.Bot.SendTextMessageAsync(messageInfo.Chat.Id, "Ты пока не подписался на рассылку");

            IsExecuted = true;
        }
    }
}
