using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoSampleMobile.Services
{
    public interface ILocalNotificationService
    {
        bool Send(string title, string description, int badge);
    }
}
