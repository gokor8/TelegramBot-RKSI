using RKSI_bot.WindowsInteractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests.WindowsInteractions
{
    public class FileTelegramApi_Test
    {
        [Fact]
        public void GetApiKey_DefaultPath_ReturnedApiKey()
        {
            string apiKey = new FileTelegramApi().GetApiKey();

            Assert.Contains(":AAH_", apiKey);
        }

        [Fact]
        public void GetApiKey_SetPath_ReturnedApiKey()
        {
            string apiKey = new FileTelegramApi(@"C:\Users\gvala\OneDrive\Рабочий стол").GetApiKey();

            Assert.Contains(":AAH_", apiKey);
        }
    }
}
