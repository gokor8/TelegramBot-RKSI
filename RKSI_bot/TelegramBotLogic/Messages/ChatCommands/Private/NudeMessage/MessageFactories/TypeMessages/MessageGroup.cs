using RKSI_bot.TelegramBotClasses.Keyboards.UserKeyboard;
using RKSI_bot.TelegramElements;
using RKSI_bot.Web;
using System.Linq;
using RKSI_bot.SchedulesContainer;
using RKSI_bot.Databases.PathDB;
using System.Collections.Generic;

namespace RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage
{
    public class MessageGroup : IMessageType
    {
        private DataBase userGroup;
        private GroupsDataStore groupsContainer;

        public MessageGroup(IPathDB pathDB)
        {
            userGroup = new DataBase(pathDB);

            groupsContainer = GroupsDataStore.GetInstance();
        }

        public bool CheckTrigger(string message)
        {
            return message.Any(x=>char.IsDigit(x));
        }

        public void Invoke(string message, long chatId)
        {
            string foundGroup = groupsContainer.Titels.FirstOrDefault(g => message.Equals(g.ToUpper().Replace("B", "").Replace("W", "")));

            if (foundGroup != null)
            {
                foundGroup = groupsContainer.Titels.FirstOrDefault(g => g.Contains(foundGroup));

                RefreshKeyboard(foundGroup, chatId);
                HttpRKSI.GetInstace().SendScheduleMessage(foundGroup, chatId, new GroupsSchedule()).Wait();
            }
            else
            {
                TelegramBot.Bot.SendTextMessageAsync(chatId, "Такой группы нету");
            }
        }

        private void RefreshKeyboard(string message, long chatId)
        {
            TelegramUserKeyboard userKeyboard = new TelegramUserKeyboard(new DefaultButtons());

            object group = userGroup.ExcecuteCommand($"SELECT Facult FROM ButtonFacultTable WHERE ChatId = '{chatId}'");

            if (group is null)
            {
                if (userGroup.GetBool(userGroup.ExcecuteCommand($"INSERT INTO ButtonFacultTable(Facult,ChatId) VALUES (N'{message}','{chatId}')")))
                {
                    userKeyboard.AddButton(message);
                    var markupKeyboard = userKeyboard.GetKeyboard();

                    TelegramBot.Bot.SendTextMessageAsync(chatId, "Группа добавлена на клавиатуру", replyMarkup: markupKeyboard);
                }
            }
            else if (!group.ToString().Contains(message))
            {
                if (userGroup.GetBool(userGroup.ExcecuteCommand($"UPDATE ButtonFacultTable SET Facult = N'{message}' WHERE ChatId = '{chatId}'")))
                {
                    userKeyboard.ReplaceButton(group.ToString(), message);
                    var markupKeyboard = userKeyboard.GetKeyboard();

                    TelegramBot.Bot.SendTextMessageAsync(chatId, "Группа обновлена на клавиатуре", replyMarkup: markupKeyboard);
                }
            }
        }
    }
}
