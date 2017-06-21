using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoSampleMobile.Services.RSS
{
    public class News : FeedBase
    {
        public News()
        {
           Category = Comments = string.Empty;
        }
        //public override void GetExt(System.ServiceModel.Syndication.SyndicationElementExtensionCollection myext)
        //{
        //    base.GetExt(myext);
        //}
        [DefaultValue(true)]
        public bool Published { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string Category { get; set; }

        public string Comments { get; set; }
        public string TaskId { get; set; }

    }
}
