﻿using RKSI_bot.SchedulesContainer;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RKSI_bot.TelegramBotClasses.Keyboards
{
    class FormationGroup
    {
        private GroupsDataStore GroupsContainer;

        public FormationGroup()
        {
            GroupsContainer = GroupsDataStore.GetInstance();
        }

        public List<string> GetCoursGroups(int cours)
        {
            List<string> groups = new List<string>();

            foreach (var group in GroupsContainer.Titels)
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