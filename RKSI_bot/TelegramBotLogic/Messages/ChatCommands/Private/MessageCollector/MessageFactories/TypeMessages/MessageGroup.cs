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
        public bool IsSended { get; private set; }

        public bool CheckTrigger(string message)
        {
            return message.Any(x => char.IsDigit(x));
        }

        public void Invoke(string message, string keyWord, long chatId)
        {
            string foundGroup = groupsContainer.GetClearTitels().FirstOrDefault(g => message.Equals(g.ToUpper()));

            if (foundGroup != null)
            {
                foundGroup = groupsContainer.GetTitels().FirstOrDefault(g => g.Contains(foundGroup));

                RefreshKeyboard(keyWord + foundGroup, chatId);
                HttpRKSI.GetInstace().SendScheduleMessage(foundGroup, chatId, new GroupsSchedule()).Wait();
                IsSended = true;
            }
            else
            {
                TelegramBot.Bot.SendTextMessageAsync(chatId, "Такой группы нету").Wait();
                IsSended = false;
            }
        }

        private void RefreshKeyboard(string message, long chatId)
        {
            TelegramUserKeyboard userKeyboard = new TelegramUserKeyboard();

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
            else if (!group.ToString().ToUpper().Equals(message.ToUpper()))
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
