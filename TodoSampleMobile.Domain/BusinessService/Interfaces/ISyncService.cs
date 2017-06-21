using System.Threading.Tasks;

namespace TodoSampleMobile.Domain.BusinessService.Interfaces
{
    public interface ISyncService
    {
        bool IsSyncing { get; set; }

        Task SyncAll();

        Task PurgeDataAsync();
    }
}
