namespace TodoSampleMobile.Domain.AzureModels
{
    public class TodoItem : BaseAzureModel
    {
        public string Title { get; set; }
        public bool Done { get; set; }
        public string Discription { get; set; }
        public string UserId { get; set; }
    }
}