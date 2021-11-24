using RKSI_bot.TelegramBotClasses.Keyboards.UserKeyboard;
using RKSI_bot.TelegramElements;
using RKSI_bot.Web;
using RKSI_bot.Web.Https;
using System.Linq;
using System.Text.RegularExpressions;
using RKSI_bot.SchedulesContainer;
using RKSI_bot.Databases.PathDB;

namespace RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage
{
    public class MessageGroup : IMessageType
    {
        private DataBase userGroup;
        private GroupsContainer groupsContainer;

        public MessageGroup(IPathDB pathDB)
        {
            userGroup = new DataBase(pathDB);

            groupsContainer = GroupsContainer.GetInstance();
        }

        public bool CheckTrigger(string message)
        {
            return message.Any(x=>char.IsDigit(x));
        }

        public void Invoke(string message, long chatId)
        {
            string messageUnEscaped = message.Replace("пары:", "").Replace(" ","").ToUpper();

            if (groupsContainer.GroupsTitels.FirstOrDefault(t => t.ToUpper().Trim().Equals(messageUnEscaped)) != null)
            {
                RefreshKeyboard(message, chatId);
                HttpRKSI.GetInstace().SendScheduleMessage(messageUnEscaped, chatId, new GroupsSchedule()).Wait();
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
