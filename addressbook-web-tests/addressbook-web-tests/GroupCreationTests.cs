﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            OpenGroupsPage();
            InitGroupCreation();
            GroupData group = new GroupData("martishka");
            group.Header = "makaka";
            group.Footer = "shimpanse";
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupPage();
            Logout();
        }
    }
}
