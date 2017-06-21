using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;

namespace TodoSampleMobile.UITest
{

    public interface ILoginScreen
    {
        Func<AppQuery, AppQuery> loginButton { get; }
    }
}
