namespace TodoSampleMobile.Services.RSS
{
    public class FeedBase
    {
        public FeedBase()
        {
            Title = Author = PubDate = ImageUrl = Summary = Description = Url = string.Empty;
        }

        public int Id { get; set; } 
        public string Title { get; set; }
        public string Author { get; set; }
        public string PubDate { get; set; }
        public string ImageUrl { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
