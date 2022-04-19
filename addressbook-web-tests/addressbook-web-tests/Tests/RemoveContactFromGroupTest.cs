using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace addressbook_web_tests.Tests
{
    public class RemoveContactFromGroupTest : AuthTestBase
    {
        [Test]
        public void TestRemoveContactFromGroup()
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

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();

            if (oldList.Count == 0)
            {
                ContactData con = new ContactData("Evgenii", "Petrosyan");
                app.Contacts.CreateContact(con);
                oldContactList.Add(con);
                app.Contacts.AddContactToGroup(con, group);
            }

            ContactData contact = ContactData.GetAll().First();

            if (!CheckContactExistInGroup(contact, group))
            {
                app.Contacts.AddContactToGroup(contact, group);
            }

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
        public static bool CheckContactExistInGroup(ContactData contact, GroupData group)
        {
            bool contactExist = false;
            List<ContactData> contactListForGroup = group.GetContactsForGroup(group.Id);
            foreach (ContactData c in contactListForGroup)
            {
                if (c.Id == contact.Id)
                {
                    contactExist = true;
                }
            }
            return contactExist;
        }
    }
}
