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
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
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
