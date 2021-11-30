using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.TelegramBotClasses.Keyboards.UserKeyboard
{
    class DefaultButtons
    {
        private List<string> buttons;

        public DefaultButtons()
        {
            buttons = new List<string>();

            buttons.Add("🦾 Помощь");
            buttons.Add("👩‍🏫 Рассылка");
            buttons.Add("🎃 Моя группа в рассылке");
            buttons.Add("🕴 Список групп");
        }

        public List<string> GetList()
        {
            return buttons;
        }
    }
}
