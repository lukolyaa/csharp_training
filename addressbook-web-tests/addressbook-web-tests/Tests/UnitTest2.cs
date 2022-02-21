using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class СontactCreationTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Evgenii");
            contact.Lastname = "Petrosyan";

            app.Contacts.CreateContact(contact);
            app.Navigator.Logout();
        }
        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("");
            contact.Lastname = "";

            app.Contacts.CreateContact(contact);
            app.Navigator.Logout();
        }
    }
}
