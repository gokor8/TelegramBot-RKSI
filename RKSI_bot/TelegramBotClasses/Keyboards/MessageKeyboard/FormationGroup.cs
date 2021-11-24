using RKSI_bot.SchedulesContainer;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RKSI_bot.TelegramBotClasses.Keyboards
{
    class FormationGroup
    {
        private GroupsContainer GroupsContainer;

        public FormationGroup()
        {
            GroupsContainer = GroupsContainer.GetInstance();
        }

        public List<string> GetCoursGroups(int cours)
        {
            List<string> groups = new List<string>();

            foreach (var group in GroupsContainer.GroupsTitels)
            {
                string cleanCours = Regex.Replace(group, @"[^0-9]", "");

                if (Convert.ToInt32(cleanCours[0].ToString()) == cours)
                {
                    groups.Add(group);
                }
            }

            return groups;
        }
    }
}
