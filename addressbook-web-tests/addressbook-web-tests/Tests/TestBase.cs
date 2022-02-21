using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class TestBase //ссылки на вспомогательные классы
    {
        protected ApplicationManager app;

        [SetUp]
        public void SetupTest() //метод для инициализации
        {
            app = new ApplicationManager();
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
        }
        [TearDown]
        public void TeardownTest()
        {
            app.Stop();
        }

    }

}