using RKSI_bot.Groups;

namespace RKSI_bot
{
    static class GroupsContainer
    {
        public static string[] Groups;

        // Можно было бы избавиться от статики, и реализовать бд, вместо Excel, и каждый раз запрашивать.
        // Но пока оставлю excel для демонстрации работы с ним, а далее будет переход на ДБ.
        static GroupsContainer()
        {
            Groups = new ExcelGroups(@"D:\Users\gzaly\OneDrive\Рабочий стол\Groups.xlsx").GetDataExcel(false)[0][0];
        }
    }
}