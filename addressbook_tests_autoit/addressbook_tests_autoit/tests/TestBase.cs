using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace autoit_addressbook
{
    public class TestBase
    {
        public ApplicationManager app;

        [OneTimeSetUp]
        public void initApplication()
        {
            app = new ApplicationManager();
        }

        [OneTimeTearDown]
        public void stopApplication()
        {
            app.Stop();
        }
    }
}