namespace TodoSampleMobile.Domain
{
    public class Constants
    {
        public const string OfflineDbName = @"localstore.db";
        public const string ApplicationKey = @"";
        //public const string NewsUrl = "http://api.geonames.org/rssToGeoRSS?feedUrl=http://feeds.reuters.com/reuters/AfricaEgyptNews?format=xml&username=mezzat";
        public const string GeoRssUrl = "http://api.geonames.org/rssToGeoRSS?feedUrl=$&username=mezzat";
        public const string GoogleNewsUrl = "https://news.google.com/news?output=rss&geo=$";

        #region Q U E R I E S

        public const string UserQueryName = "allUsers";
        public const string TodoQueryName = "allTodoItems";
        #endregion
    }
}