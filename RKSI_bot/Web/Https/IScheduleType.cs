using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RKSI_bot.Web.Https
{
    public interface IScheduleType
    {
        Task<string> SendRKSI(string currentEncoding);
    }
}
