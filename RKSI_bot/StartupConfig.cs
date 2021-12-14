using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;

namespace RKSI_bot
{
    public class StartupConfig
    {
        private static StartupConfig startup = new StartupConfig();
        public IConfiguration Configuration { get; }

        public StartupConfig()
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        }

        public static StartupConfig GetInstance()
        {
            return startup;
        }
    }
}
