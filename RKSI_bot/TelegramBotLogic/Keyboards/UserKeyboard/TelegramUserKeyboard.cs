using RKSI_bot.TelegramBotClasses.Keyboards.UserKeyboard;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace RKSI_bot.TelegramElements
{
    class TelegramUserKeyboard
    {
        private List<string> _buttons = new DefaultButtons().GetList();

        public void AddButton(string text)
            => _buttons.Add(text);

        public void ReplaceButton(string replaceText, string newText)
        {
            _buttons.Remove(replaceText);
            AddButton(newText);
        }

        public ReplyKeyboardMarkup GetKeyboard()
        {
            int buttonsCount = _buttons.Count;
            int countRows = buttonsCount % 2 == 0 ? buttonsCount / 2 : (buttonsCount / 2) + 1;

            KeyboardButton[][] buttonsArray = new KeyboardButton[countRows][];

            for (int keyboardRow = 0; keyboardRow < _buttons.Count;)
            {
                int addRow = 2;

                if (keyboardRow + addRow > buttonsCount)
                    addRow = 1;

                KeyboardButton[] button = new KeyboardButton[addRow];

                for (int keyboarButton = 0; keyboarButton < addRow; keyboarButton++)
                {
                    button[keyboarButton] = _buttons[keyboardRow + keyboarButton];
                }

                buttonsArray[keyboardRow/2] = button;

                keyboardRow += addRow;
            }

            return new ReplyKeyboardMarkup(buttonsArray,resizeKeyboard:true);
        }
    }
}
