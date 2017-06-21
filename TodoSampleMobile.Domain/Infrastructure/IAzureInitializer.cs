using System.Threading.Tasks;

namespace TodoSampleMobile.Domain.Infrastructure
{
    public interface IAzureInitializer
    {
        Task SyncClientAndStore();
    }
}