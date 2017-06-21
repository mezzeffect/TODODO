using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoSampleMobile.Services.RSS
{
    public interface IRssManger<T> where T : News, new()
    {
        Task<List<T>> GetFeed(string urlEn, string urlAr);
    }
}