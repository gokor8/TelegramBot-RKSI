using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage
{
    public interface IMessageType
    {
        bool CheckTrigger(string message);

        void Invoke(string message, long chatId);
    }
}
