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
            if (GroupData.GetAll().Count == 0)
            {
                GroupData g = new GroupData("something");
                app.Groups.Create(g);
            }

            if (ContactData.GetAll().Count == 0)
            {
                ContactData c = new ContactData("some", "thing");
                app.Contacts.CreateContact(c);
            }

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            if (contact == null)
            {
                ContactData con = new ContactData("Evgenii", "Petrosyan");
                app.Contacts.CreateContact(con);
                contact = ContactData.GetAll().Except(group.GetContacts()).First();
            }

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}