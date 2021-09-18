using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.TelegramBotClasses.Keyboards.UserKeyboard.Buttons
{
    interface IButtons
    {
        List<string> GetList();
    }
}
