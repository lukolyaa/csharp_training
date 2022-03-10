using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData NewData = new GroupData("samolet");
            NewData.Header = "vertolet";
            NewData.Footer = "paraplan";

            app.Groups.Modify(1, NewData);
        }
    }
}
