namespace TodoSampleMobile.Domain.AzureModels
{
    public class User : BaseAzureModel
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    
    }
}