using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramBotLogic.Messages;
using System.Collections.Generic;
using Telegram.Bot.Args;

namespace RKSI_bot.Commands.Commands_Objects
{
    internal class Group : ICommand
    {
        public string[] Triggers { get; set; }
        public bool IsExecuted { get; private set; }
        private List<long> _spamIds = SpamDataStore.GetInstace().MessageIds;

        public Group(params string[] triggers)
        {
            Triggers = triggers;
        }

        public void Execute(Telegram.Bot.Types.Message messageInfo)
        {
            _spamIds.Add(messageInfo.Chat.Id);
            TelegramBot.Bot.SendTextMessageAsync(messageInfo.Chat.Id, "Введите свою группу (Пример: ЧПОКС-51)").Wait();

            IsExecuted = true;
        }
    }
}