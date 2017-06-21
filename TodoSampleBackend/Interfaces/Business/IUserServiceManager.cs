using TodoSampleBackend.Business;
using TodoSampleBackend.DataObjects;

namespace TodoSampleBackend.Interfaces.Business
{
    public interface IUserServiceManager : IBusinessManager<User>
    {
        UserBusinessObject Authenticate(LoginBusinessObject loginReuest);
        User GetUserByUsername(string userName);
    }
}