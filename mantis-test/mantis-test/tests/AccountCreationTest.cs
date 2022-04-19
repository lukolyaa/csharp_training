using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [OneTimeSetUp]
        public void SetUpConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open(TestContext.CurrentContext.TestDirectory + "/config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localFile);
            }
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "Evgenii",
                Password = "Password",
                Email = "Evgenii@evg.ru"
            };

            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);
        }

        [OneTimeTearDown]
        public void RestoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }
    }
}