using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TodoSampleMobile.Services.RSS
{
    public class RssManger<T> : IRssManger<T> where T : News, new()
    {
        public async Task<List<T>> GetFeed(string urlEn, string urlAr)
        {
            //Put all the feed items into this list  
            var feed = new List<T>();
            Stream stream = null;
            //WebClient is used in case the computer is sitting behind a proxy  
            var client = new HttpClient();
            try
            {
                stream = await client.GetStreamAsync(urlEn);
            }
            catch (Exception)
            {


            }

            if (stream != null)
            {
                Rss deserialized = null;
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Rss));
                    StreamReader reader = new StreamReader(stream);
                    string text = reader.ReadToEnd();
                    text = text.Replace("<geo:", "<").Replace("</geo:", "</");
                    var tr = new StringReader(text);
                    //var t = serializer.Deserialize(tr);
                    deserialized = (Rss)serializer.Deserialize(tr);
                }
                catch
                {

                }

                if (deserialized != null)
                    foreach (var item in deserialized.channel.item)
                    {
                        try
                        {
                            var feedItem = new T
                            {
                                Title = item.title ?? string.Empty,
                               // Author = item.author == null ? item.author : "",
                                Url = item.link,
                                //ImageUrl = item.image,
                                Category = item.category ?? string.Empty,
                                //Comments = item.comments ?? string.Empty
                                Latitude = item.Latitude,
                                Longitude = item.Longitude

                            };
                            if (item.description != null)
                            {
                                
                                feedItem.Description = Regex.Replace(item.description.Text[0], @"<[^>]*>", string.Empty,RegexOptions.None).Trim();
                                feedItem.Description = RemoveSpecialCharacters(feedItem.Description);
                            }

                            if (item.pubDate != null)
                                feedItem.PubDate = DateTime.Parse(item.pubDate).ToLocalTime().ToString();
                            feed.Add(feedItem);
                        }
                        catch (Exception)
                        {
                            feed.Add(new T());
                        }
                    }
            }


            try
            {
                stream =  await client.GetStreamAsync(urlAr);
            }
            catch (Exception)
            {

            }
            



         
            return feed;
        }
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' '||c == ':'||c == '/')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
