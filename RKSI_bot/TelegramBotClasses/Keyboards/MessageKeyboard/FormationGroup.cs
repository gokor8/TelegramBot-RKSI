using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RKSI_bot.TelegramBotClasses.Keyboards
{
    class FormationGroup
    {
        public List<string> GetCoursGroups(int cours)
        {
            List<string> Groups = new List<string>();
            foreach (var group in GroupsContainer.Groups)
            {
                string cleanCours = Regex.Replace(group, @"[^0-9]", "");

                if (Convert.ToInt32(cleanCours[0].ToString()) == cours)
                {
                    Groups.Add(group);
                }
            }

            return Groups;
        }
    }
}
