using System;
using System.Collections.Generic;
using System.Text;

namespace RKSI_bot.TelegramBotLogic.Messages
{
    public class SpamDataStore
    {
        private static SpamDataStore dataStore = new SpamDataStore();

        public List<long> MessageIds 
        {
            get;
            private set;
        } = new List<long>();

        public static SpamDataStore GetInstace()
        {
            return dataStore;
        }
    }
}
