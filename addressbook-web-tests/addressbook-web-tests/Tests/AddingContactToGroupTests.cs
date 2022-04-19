using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            List<GroupData> oldGroupList = GroupData.GetAll();
            if (oldGroupList.Count == 0)
            {
                GroupData g = new GroupData("something");
                app.Groups.Create(g);
                oldGroupList.Add(g);
            }

            List<ContactData> oldContactList = ContactData.GetAll();
            if (oldContactList.Count == 0)
            {
                ContactData c = new ContactData("some", "thing");
                app.Contacts.CreateContact(c);
                oldContactList.Add(c);
            }

            var group = GroupData.GetAll()[0];
            var oldList = group.GetContacts();
            var contact = ContactData.GetAll().Except(oldList).First();

            if (contact == null)
            {
                ContactData con = new ContactData("Evgenii", "Petrosyan");
                app.Contacts.CreateContact(con);
                oldContactList.Add(con);
                contact = ContactData.GetAll().Except(oldList).First();
            }

            app.Contacts.AddContactToGroup(contact, group);

            var newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}