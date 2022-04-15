using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [Test]
    public void TestAddingContactToGroup()
    {
        var group = GroupData.GetAll()[0];
        var oldList = group.GetContacts();
        var contact = ContactData.GetAll().Except(oldList).First();

        Application.Contacts.AddContactToGroup(contact, group);

        var newList = group.GetContacts();
        oldList.Add(contact);
        oldList.Sort();
        newList.Sort();

        Assert.AreEqual(oldList, newList);
    }
