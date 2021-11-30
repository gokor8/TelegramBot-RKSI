using RKSI_bot.SchedulesContainer;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RKSI_bot.TelegramBotClasses.Keyboards
{
    class KeyboardGroupsCollector
    {
        private GroupsDataStore GroupsContainer;

        public KeyboardGroupsCollector()
        {
            GroupsContainer = GroupsDataStore.GetInstance();
        }

        public List<string> GetListGroups(int cours)
        {
            List<string> groups = new List<string>();

            foreach (var group in GroupsContainer.GetTitels())
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
