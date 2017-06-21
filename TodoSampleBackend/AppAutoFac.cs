using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;

namespace TodoSampleBackend
{
    public class AppAutoFac
    {
        public static IContainer Container { get; set; }


        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}