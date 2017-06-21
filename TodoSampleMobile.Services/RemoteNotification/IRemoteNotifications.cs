using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace TodoSampleMobile.Services.RemoteNotification
{
    public interface IRemoteNotifications
    {
        void RegisterForRemoteNotifications();
    }
}