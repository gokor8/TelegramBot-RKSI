using RKSI_bot.ReservingObjects;
using System.Collections.Generic;
using Telegram.Bot.Args;

namespace RKSI_bot.Commands.Commands_Objects
{
    internal class Group : ICommand
    {
        public List<string> ChatPermissions { get; set; }
        public string[] Triggers { get; set; }

        public void Execute(ref MessageEventArgs message)
        {
            TelegramBot.Bot.SendTextMessageAsync(message.Message.Chat.Id, "Введите свою группу (Пример: ЧПОКС-51)");
            CommandsHandler.SpamIds.Add(message.Message.Chat.Id);
        }
    }
}