using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData NewData = new ContactData("Elena");
            NewData.Middlename = "Grigorievna";
            NewData.Lastname = "Stepanenko";

            app.Contacts.Modify(3, 8, NewData);
        }
    }
}
