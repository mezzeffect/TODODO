using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace TodoSampleBackend.DataObjects
{
    public class TodoItem: EntityData
    {
        public string Title { get; set; }
        public bool Done { get; set; }
        public string Discription { get; set; }
        public string UserId { get; set; }
    }
}