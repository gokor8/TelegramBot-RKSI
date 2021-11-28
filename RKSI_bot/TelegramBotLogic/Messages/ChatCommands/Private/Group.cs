using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramBotLogic.Messages;
using System.Collections.Generic;
using Telegram.Bot.Args;

namespace RKSI_bot.Commands.Commands_Objects
{
    internal class Group : ICommand
    {
        public string[] Triggers { get; set; }

        private List<long> _spamIds = SpamDataStore.GetInstace().MessageIds;

        public Group(params string[] triggers)
        {
            Triggers = triggers;
        }

        public void Execute(MessageEventArgs messageInfo)
        {
            _spamIds.Add(messageInfo.Message.Chat.Id);
            TelegramBot.Bot.SendTextMessageAsync(messageInfo.Message.Chat.Id, "Введите свою группу (Пример: ЧПОКС-51)");
        }
    }
}