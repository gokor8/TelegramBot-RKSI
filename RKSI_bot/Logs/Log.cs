using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace RKSI_bot.Logs
{
    public abstract class Log
    {
        public bool IsWrited { get; protected set; }
        public void SetLog(Message messageInfo)
        {
            var message = messageInfo;

            string textLog = message.Text + $"|| {message.Chat?.Id} - {message.Chat?.FirstName} : {message.Chat?.LastName} -- {DateTime.Now} | Username - {message.Chat?.Username}";

            PrintLog(textLog);
        }

        public void SetLog(CallbackQueryEventArgs messageInfo)
        {
            var message = messageInfo.CallbackQuery.Message;

            string textLog = message.Text + $"|{message.Chat?.Username} | {message.Chat?.Id} - {message.Chat?.FirstName} : {message.Chat?.LastName} -- {DateTime.Now} | Username - {message.Chat?.Username}";

            PrintLog(textLog);
        }

        protected abstract void PrintLog(string message);
    }
}
