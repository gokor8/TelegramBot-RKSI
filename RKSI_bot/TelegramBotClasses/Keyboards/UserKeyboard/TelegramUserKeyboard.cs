using RKSI_bot.TelegramBotClasses.Keyboards.UserKeyboard.Buttons;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace RKSI_bot.TelegramElements
{
    class TelegramUserKeyboard
    {
        public TelegramUserKeyboard(IButtons userKeyboardButtons)
        {
            buttons = userKeyboardButtons.GetList();
        }

        private List<string> buttons;

        public void AddButton(string text)
            => buttons.Add(text);

        public void ReplaceButton(string replaceText, string newText)
        {
            buttons.Remove(replaceText);
            AddButton(newText);
        }

        public ReplyKeyboardMarkup GetKeyboard()
        {
            int buttonsCount = buttons.Count;
            int countRows = buttonsCount % 2 == 0 ? buttonsCount / 2 : (buttonsCount / 2) + 1;

            KeyboardButton[][] buttonsArray = new KeyboardButton[countRows][];

            for (int keyboardRow = 0; keyboardRow < buttons.Count;)
            {
                int addRow = 2;

                if (keyboardRow + addRow > buttonsCount)
                    addRow = 1;

                KeyboardButton[] button = new KeyboardButton[addRow];

                for (int keyboarButton = 0; keyboarButton < addRow; keyboarButton++)
                {
                    button[keyboarButton] = buttons[keyboardRow + keyboarButton];
                }

                buttonsArray[keyboardRow/2] = button;

                keyboardRow += addRow;
            }

            return new ReplyKeyboardMarkup(buttonsArray,resizeKeyboard:true);
        }
    }
}
