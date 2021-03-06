using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            int num = 0;
            app.Contacts.ContactExistenceCheck();

            ContactData NewData = new ContactData("Elena", "Stepanenko");
            NewData.Middlename = "Grigorievna";

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData oldData = oldContacts[num];

            app.Contacts.Modify(oldData, NewData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[num].Firstname = NewData.Firstname;
            oldContacts[num].Lastname = NewData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach(ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(NewData.Firstname, contact.Firstname);
                }
            }
        }
    }
}
