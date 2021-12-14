using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RKSI_bot.WindowsInteractions
{
    public class FileTelegramApi
    {
        private readonly string _pathToFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BotToken.txt";

        public FileTelegramApi()
        { }
        public FileTelegramApi(string pathToFile)
        {
            if (!pathToFile.Contains(".txt"))
                _pathToFile = $@"{pathToFile}\BotToken.txt";
        }
        
        public string GetApiKey()
        {
            return File.ReadAllText(_pathToFile);
        }
    }
}
