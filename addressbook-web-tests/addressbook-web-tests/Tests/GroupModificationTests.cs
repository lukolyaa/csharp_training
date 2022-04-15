using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            int num = 0;
            app.Groups.GroupExistenceCheck();

            GroupData NewData = new GroupData("samolet");
            NewData.Header = "vertolet";
            NewData.Footer = "paraplan";

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData oldData = oldGroups[num];

            app.Groups.Modify(oldData, NewData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = NewData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(NewData.Name, group.Name);
                }
            }
        }
    }
}
