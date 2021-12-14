using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage
{
    public interface IMessageType
    {
        bool IsSended { get; }
        bool CheckTrigger(string message);

        // keyword служит ключевым словом(пары ), котрое понимают группы.
        // При обновлении и добавлении на клавиатуру, оно будет соединяться с группой
        // Поэтому группы будут понимать клавиатурную кнопку
        void Invoke(string message, string keyword, long chatId);
    }
}
