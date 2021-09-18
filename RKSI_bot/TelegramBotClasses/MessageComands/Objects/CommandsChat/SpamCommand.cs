using RKSI_bot.Databases.PathDB;
using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace RKSI_bot.Comand_Message.Commands_Objects
{
    internal class SpamCommand
    {
        //private MessageEventArgs message;
        private List<string> chatTypes;
        private List<long> spamIds;

        private long chatId;
        private string message;

        public SpamCommand(List<long> spamIds)
        {
            chatTypes = new List<string> { "group", "supergroup", "private" };
            this.spamIds = spamIds;
        }

        public async Task Subscribe(string message, long chatId)
        {
            this.chatId = chatId;
            this.message = message;

            DataBase dataBase = new DataBase(message, chatId, new LocalPathDB("Database"));

            if (GroupsContainer.Groups.FirstOrDefault(t => t.ToUpper().Trim().Equals(message)) != null)
            {
                // N для киррилицы ''для значений
                if (dataBase.GetBool(dataBase.ExcecuteCommand($"SELECT 1 FROM ttable WHERE id_person={chatId}")))
                {
                    bool canUpdate = dataBase.GetBool(dataBase.ExcecuteCommand($"UPDATE ttable SET Facult=N'{message.ToUpper()}' WHERE id_person='{chatId}'"));
                    if (canUpdate)
                        await TelegramBot.Bot.SendTextMessageAsync(chatId, "Ваша группа изменена в ежедневном расписании");
                }
                else
                {
                    bool canAdd = dataBase.GetBool(dataBase.ExcecuteCommand($"INSERT INTO ttable(id_person,Facult) VALUES ('{chatId}',N'{message}')"));
                    if (canAdd)
                        await TelegramBot.Bot.SendTextMessageAsync(chatId, "Вы подписались на рассылку расписания");
                }
            }
            else
                await TelegramBot.Bot.SendTextMessageAsync(chatId, "Такой группы нету");

            spamIds.Remove(chatId);
        }

        public bool HasChatType(string chatType)
        {
            foreach (var findchattype in chatTypes)
            {
                if (chatType.Equals(findchattype))
                    return true;
            }
            return false;
        }
    }
}