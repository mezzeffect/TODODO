using TodoSampleBackend.Models;
using Newtonsoft.Json;
using TodoSampleBackend.Business;

namespace TodoSampleBackend.Models
{
    public class LoginRepsponse:BaseResponse
    {
        [JsonProperty(PropertyName = "authenticationToken")]
        public string AuthenticationToken { get; set; }

        [JsonProperty(PropertyName = "user")]
        public UserModel User { get; set; }
    }
}