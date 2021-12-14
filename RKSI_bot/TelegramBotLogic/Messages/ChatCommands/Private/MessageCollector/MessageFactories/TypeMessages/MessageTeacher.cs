using RKSI_bot.SchedulesContainer;
using RKSI_bot.Web;
using System.Linq;

namespace RKSI_bot.TelegramBotClasses.MessageComands.Objects.CommandsChannel.NudeMessage
{
    public class MessageTeacher : IMessageType
    {
        private TeachersDataStore teachersContainer;
        public MessageTeacher()
        {
            teachersContainer = TeachersDataStore.GetInstance();
        }

        public bool IsSended { get; private set; }

        public bool CheckTrigger(string message)
        {
            return message.Replace(" ", "").All(c => char.IsLetter(c) || c == '.');
        }

        public void Invoke(string message, string keyWord, long chatId)
        {
            string foundTeacher = teachersContainer.GetTitels().FirstOrDefault(t => t.ToUpper().Contains(message));

            if (foundTeacher != null)
            {
                HttpRKSI.GetInstace().SendScheduleMessage(foundTeacher, chatId, new TeachersSchedule()).Wait();
                IsSended = true;
            }
            else
            {
                TelegramBot.Bot.SendTextMessageAsync(chatId, "Такого преподавателя нету").Wait();
                IsSended = false;
            }
        }
    }
}
