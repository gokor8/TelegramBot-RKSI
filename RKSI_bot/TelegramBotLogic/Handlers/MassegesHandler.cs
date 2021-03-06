using RKSI_bot.ReservingObjects;
using RKSI_bot.TelegramBotClasses.Messages;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RKSI_bot
{
    internal class MassegesHandler
    {
        private CommandsHandler commands;

        public MassegesHandler()
        {
            commands = new CommandsHandler();
        }

        public void OnMessage(Message messageInformation)
        {
            Task.Run(async () =>
            {
                long chatId = messageInformation.Chat.Id;
                var messageType = messageInformation.Type;

                if (messageType != MessageType.Text)
                {
                    string say = RandomSay();
                    await TelegramBot.Bot.SendTextMessageAsync(chatId, say + ". Введите текстовый запрос");
                }
                else
                {
                    await commands.Invoke(messageInformation);
                }
            });
        }

        private string RandomSay()
        {
            string[] randoms = new string[1];
            randoms[0] = "Безделье-это игрушка дъявола";
            randoms[1] = "Не надо там по углам: курить, шабить";

            return randoms[new Random().Next(0, randoms.Length)];
        }
    }
}