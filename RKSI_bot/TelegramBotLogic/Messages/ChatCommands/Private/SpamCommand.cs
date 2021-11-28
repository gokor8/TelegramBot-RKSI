using RKSI_bot.Databases.PathDB;
using RKSI_bot.SchedulesContainer;
using RKSI_bot.TelegramBotLogic.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace RKSI_bot.Comand_Message.Commands_Objects
{
    public class SpamCommand
    { 
        private GroupsDataStore groupsContainer = GroupsDataStore.GetInstance();

        public readonly List<ChatType> chatTypes = new List<ChatType> { ChatType.Private };
        private List<long> spamIds = SpamDataStore.GetInstace().MessageIds;

        public async Task Subscribe(string message, long chatId)
        {
            DataBase dataBase = new DataBase(message, chatId, new LocalPathDB("Database"));

            message = groupsContainer.Titels.FirstOrDefault(g => message.ToUpper().Equals(g.ToUpper().Replace("B", "").Replace("W", "")));

            if (message != null)
            {
                message = groupsContainer.Titels.FirstOrDefault(g => g.Contains(message));
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
            }
            else
                await TelegramBot.Bot.SendTextMessageAsync(chatId, "Такой группы нету");

            spamIds.Remove(chatId);
        }
    }
}