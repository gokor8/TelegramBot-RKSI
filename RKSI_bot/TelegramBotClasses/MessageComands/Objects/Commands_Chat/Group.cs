using RKSI_bot.ReservingObjects;
using System.Collections.Generic;
using Telegram.Bot.Args;

namespace RKSI_bot.Commands.Commands_Objects
{
    internal class Group : ICommand
    {
        public List<string> ChatPermissions { get; set; }
        public string[] Triggers { get; set; }
        private long chatId;

        private List<long> spamIds;

        public Group(List<long> spamIds)
        {
            this.spamIds = spamIds;
        }

        public void Execute(ref MessageEventArgs message)
        {
            spamIds.Add(chatId);
            chatId = message.Message.Chat.Id;
            TelegramBot.Bot.SendTextMessageAsync(message.Message.Chat.Id, "Введите свою группу (Пример: ЧПОКС-51)");
        }
    }
}