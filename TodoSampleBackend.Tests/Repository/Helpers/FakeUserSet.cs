using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoSampleBackend.DataObjects;

namespace TodoSampleBackend.Tests.Helpers
{
    public class FakeUserSet : FakeDbSet<User>
    {
        public override User Find(params object[] keyValues)
        {
            return this.SingleOrDefault(g => g.Id == (string)keyValues.Single());
        }

    }
}
