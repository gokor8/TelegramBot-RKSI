using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramElements;
using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace RKSI_bot.Comand_Message.Objects.Commands_Group_Objects
{
    internal class Message : ICommand
    {
        public List<string> ChatPermissions { get; set; }
        public string[] Triggers { get; set; }

        public void Execute(ref MessageEventArgs chatInformation)
        {
            string messageUnEscaped = chatInformation.Message.Text.Replace("пары:", "").Replace(" ", "").ToUpper().Trim();
            long chatId = chatInformation.Message.Chat.Id;

            RefreshKeyboard(messageUnEscaped, chatId);

            HttpRKSI.SendScheduleMessage(messageUnEscaped, chatId);
        }

        private void RefreshKeyboard(string message, long chatId)
        {
            DataBase userGroup = new DataBase("DatabaseOftenGroup");

            object group = userGroup.ExcecuteCommand($"SELECT UserGroup FROM ButtonTable WHERE UserChatID = '{chatId}'");

            if (group is null)
            {
                if (userGroup.GetBool(userGroup.ExcecuteCommand($"INSERT INTO ButtonTable(UserGroup,UserChatID) VALUES (N'{message}','{chatId}')")))
                {
                    TelegramUserKeyboard userKeyboard = new TelegramUserKeyboard();

                    userKeyboard.AddButton(message);

                    var usersKeyboard = new TelegramUserKeyboard().GetKeyboard();

                    TelegramBot.Bot.SendTextMessageAsync(chatId, "A", replyMarkup: usersKeyboard);
                }
            }
            else if(!group.ToString().Contains(message))
            {
                if (userGroup.GetBool(userGroup.ExcecuteCommand($"UPDATE ButtonTable SET UserGroup = N'{message}' WHERE UserChatId = '{chatId}'")))
                {
                    TelegramUserKeyboard userKeyboard = new TelegramUserKeyboard();

                    userKeyboard.ReplaceButton(group.ToString(), message);

                    var usersKeyboard = userKeyboard.GetKeyboard();

                    TelegramBot.Bot.SendTextMessageAsync(chatId, "=======================", replyMarkup: usersKeyboard);
                }
            }

        }
    }
}