using System.ComponentModel.DataAnnotations;

namespace TodoSampleBackend.Business
{
    public class LoginBusinessObject
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}