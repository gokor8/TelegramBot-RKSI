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

        public MessageGroup(IPathDB pathDB)
        {
            userGroup = new DataBase(pathDB);
        }

        public bool CheckTrigger(string message)
        {
            return message.Any(x=>char.IsDigit(x));
        }

        public void Invoke(string message, long chatId)
        {
            string messageUnEscaped = message.Replace("пары:", "").Replace(" ","").ToUpper();

            if (Groups.GroupTitles.FirstOrDefault(t => t.ToUpper().Trim().Equals(messageUnEscaped)) != null)
            {
                RefreshKeyboard(message, chatId);
                HttpRKSI.SendScheduleMessage(messageUnEscaped, chatId, new GroupsFactory()).Wait();
            }
            else
            {
                TelegramBot.Bot.SendTextMessageAsync(chatId, "Такой группы нету");
            }
        }

        private void RefreshKeyboard(string message, long chatId)
        {
            TelegramUserKeyboard userKeyboard = new TelegramUserKeyboard(new DefaultButtons());

            object group = userGroup.ExcecuteCommand($"SELECT UserGroup FROM ButtonTable WHERE UserChatID = '{chatId}'");

            if (group is null)
            {
                if (userGroup.GetBool(userGroup.ExcecuteCommand($"INSERT INTO ButtonTable(UserGroup,UserChatID) VALUES (N'{message}','{chatId}')")))
                {
                    userKeyboard.AddButton(message);
                    var markupKeyboard = userKeyboard.GetKeyboard();

                    TelegramBot.Bot.SendTextMessageAsync(chatId, "Группа добавлена на клавиатуру", replyMarkup: markupKeyboard);
                }
            }
            else if (!group.ToString().Contains(message))
            {
                if (userGroup.GetBool(userGroup.ExcecuteCommand($"UPDATE ButtonTable SET UserGroup = N'{message}' WHERE UserChatId = '{chatId}'")))
                {
                    userKeyboard.ReplaceButton(group.ToString(), message);
                    var markupKeyboard = userKeyboard.GetKeyboard();

                    TelegramBot.Bot.SendTextMessageAsync(chatId, "Группа обновлена на клавиатуре", replyMarkup: markupKeyboard);
                }
            }
        }
    }
}
