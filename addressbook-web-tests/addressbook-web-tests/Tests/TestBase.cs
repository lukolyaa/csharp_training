using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class TestBase //ссылки на вспомогательные классы
    {
        protected ApplicationManager app;

        [SetUp]
        public void SetupApplicationManager() //метод для инициализации
        {
            app = ApplicationManager.GetInstance();
        }
    }
}