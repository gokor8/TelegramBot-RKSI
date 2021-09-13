using RKSI_bot.Groups;
using RKSI_bot.TelegramBotClasses.MessageHandlers;
using RKSI_bot.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace RKSI_bot.TelegramBotClasses
{
    class KeyboardHandler
    {
        private List<IKeyboardLogic> keyboardsLogic;
        public KeyboardHandler()
        {
            keyboardsLogic = new List<IKeyboardLogic>();
        }

        public void AddkeyboardLogic(IKeyboardLogic keyboardLogic)
            => keyboardsLogic.Add(keyboardLogic);

        public void OnKeyboard(object sender, CallbackQueryEventArgs senderData)
        {
            string message = senderData.CallbackQuery.Message.Text;

            foreach(var keyboardLogic in keyboardsLogic)
            {
                if(keyboardLogic.triggers.FirstOrDefault(trigger=>message.Contains(trigger)) != null)
                {
                    keyboardLogic.Invoke(message, senderData.CallbackQuery.Message.Chat.Id);
                }
            }
        }
    }
    
}
