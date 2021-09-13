using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.TelegramBotClasses.MessageHandlers
{
    interface IKeyboardLogic
    {
        string[] triggers { get; set; }

        void Invoke(string message, long chatId);

        Dictionary<long, int> GetEditedDictonary();
    }
}
