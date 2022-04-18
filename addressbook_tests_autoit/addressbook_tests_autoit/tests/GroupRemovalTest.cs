using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace autoit_addressbook
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void TestGroupRemoval()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            GroupData removedGroup = oldGroups[0];
            app.Groups.Remove(removedGroup);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}