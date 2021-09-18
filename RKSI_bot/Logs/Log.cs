using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;

namespace RKSI_bot.Logs
{
    abstract class Log
    {
        public void SetLog(MessageEventArgs messageInfo)
        {
            var message = messageInfo.Message;

            string textLog = message.Text + $"|| {message.Chat.Id} - {message.Chat.FirstName} : {message.Chat.LastName} -- {DateTime.Now} | Username - {message.Chat.Username}";

            PrintLog(textLog);
        }

        public void SetLog(CallbackQueryEventArgs messageInfo)
        {
            var message = messageInfo.CallbackQuery.Message;

            string textLog = message.Text + $"|{message.Chat.Username} | {message.Chat.Id} - {message.Chat.FirstName} : {message.Chat.LastName} -- {DateTime.Now} | Username - {message.Chat.Username}";

            PrintLog(textLog);
        }

        public abstract void PrintLog(string message);
    }
}
