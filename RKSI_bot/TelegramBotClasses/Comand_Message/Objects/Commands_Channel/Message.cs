using RKSI_bot.Databases.PathDB;
using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramElements;
using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace RKSI_bot.Comand_Message.Objects.Commands_Group_Objects
{
    internal class Message : ICommand
    {
        public List<string> ChatPermissions { get; set; }
        public string[] Triggers { get; set; }

        private DataBase userGroup;
        private TelegramUserKeyboard userKeyboard;

        public Message()
        {
            userGroup = new DataBase(new LocalPathDB("DatabaseOftenGroup"));
            userKeyboard = new TelegramUserKeyboard();
        }

        public void Execute(ref MessageEventArgs chatInformation)
        {
            string message = Regex.Replace(chatInformation.Message.Text, @"\s+", "");
            string messageUnEscaped = message.Replace("пары:", "").ToUpper();
            long chatId = chatInformation.Message.Chat.Id;

            if (GroupsContainer.IsConsistGroups(messageUnEscaped))
            {
                RefreshKeyboard(message, chatId);
                HttpRKSI.SendScheduleMessage(messageUnEscaped, chatId, new ParsingGroups());
            }
            else
            {
                TelegramBot.Bot.SendTextMessageAsync(chatId, "Такой группы нету");
            }
        }

        private void RefreshKeyboard(string message, long chatId)
        {
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
            else if(!group.ToString().Contains(message))
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