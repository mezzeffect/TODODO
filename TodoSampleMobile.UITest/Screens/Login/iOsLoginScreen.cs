using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;

namespace TodoSampleMobile.UITest
{
    public class iOSLoginScreen:ILoginScreen
    {
        public Func<AppQuery, AppQuery> loginButton { get; } = new Func<AppQuery, AppQuery>(c => c.Marked("Active Directory"));
    }
}
