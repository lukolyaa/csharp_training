using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests.Tests
{
    class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            int num = 0;
            app.Contacts.ContactExistenceCheck();

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Remove(num);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.RemoveAt(num);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
