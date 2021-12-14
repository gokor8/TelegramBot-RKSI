using RKSI_bot.Databases.PathDB;
using RKSI_bot.SchedulesContainer;
using RKSI_bot.TelegramBotLogic.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RKSI_bot.Comand_Message.Commands_Objects
{
    public class SpamSubcribe
    {
        private GroupsDataStore groupsDataStore = GroupsDataStore.GetInstance();
        private List<long> spamIds = SpamDataStore.GetInstace().MessageIds;

        public bool IsSubcribe { get; private set; }

        public async Task Subscribe(string message, long chatId)
        {
            DataBase dataBase = new DataBase(message, chatId, new LocalPathDb("Database"));

            message = groupsDataStore.GetClearTitels().FirstOrDefault(g => message.ToUpper().Equals(g.ToUpper()));

            if (message != null)
            {
                message = groupsDataStore.GetTitels().FirstOrDefault(g => g.Contains(message));
                // N для киррилицы '' для значений
                if (dataBase.GetBool(dataBase.ExcecuteCommand($"SELECT 1 FROM UserTable WHERE ChatId={chatId}")))
                {
                    bool canUpdate = dataBase.GetBool(dataBase.ExcecuteCommand($"UPDATE UserTable SET Facult=N'{message.ToUpper()}' WHERE ChatId='{chatId}'"));
                    if (canUpdate)
                        await TelegramBot.Bot.SendTextMessageAsync(chatId, "Ваша группа изменена в ежедневном расписании");
                }
                else
                {
                    bool canAdd = dataBase.GetBool(dataBase.ExcecuteCommand($"INSERT INTO UserTable(ChatId,Facult) VALUES ('{chatId}',N'{message}')"));
                    if (canAdd)
                        await TelegramBot.Bot.SendTextMessageAsync(chatId, "Вы подписались на рассылку расписания");
                }
                IsSubcribe = true;
            }
            else
            {
                await TelegramBot.Bot.SendTextMessageAsync(chatId, "Такой группы нету");
                IsSubcribe = false;
            }

            spamIds.Remove(chatId);
        }
    }
}