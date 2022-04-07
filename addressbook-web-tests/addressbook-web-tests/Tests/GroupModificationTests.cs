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
            app.Groups.GroupExistenceCheck();

            GroupData NewData = new GroupData("samolet");
            NewData.Header = "vertolet";
            NewData.Footer = "paraplan";

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Modify(0, NewData);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = NewData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
