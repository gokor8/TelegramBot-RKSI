using RKSI_bot;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RksiBot_Tests.Config
{
    public class StartupConfig_Test
    {
        [Fact]
        public void GetInstance_GetConnectionStrings_ReturnNotNullValue()
        {
            var config = StartupConfig.GetInstance().Configuration;

            string jsonValue = config.GetSection("ConnectionStrings")["FirstStringConnection"];

            Assert.NotNull(jsonValue);
        }
    }
}
