namespace TodoSampleMobile.Services.Authentication
{
    public class UserInfoModel
    {
        public string AccessToken { get; set; }

        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }
    }
}