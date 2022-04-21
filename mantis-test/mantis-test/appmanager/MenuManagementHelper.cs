using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class MenuManagementHelper : HelperBase
    {
        protected string baseURL;
        public MenuManagementHelper(ApplicationManager manager, string baseURL)
            : base(manager)
        {
            this.baseURL = baseURL;
        }
        public void OpenViewPage()
        {
            if (driver.Url == baseURL + "/mantisbt-2.25.3/my_view_page.php")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + "/mantisbt-2.25.3/my_view_page.php");
        }
        public void OpenManageOverviewPage()
        {
            if (driver.Url == baseURL + "mantisbt-2.25.3/manage_overview_page.php"
                && IsElementPresent(By.XPath("//th[@class='category' and contains(text(), 'Версия MantisBT')]")))
            {
                return;
            }
            driver.FindElement(By.XPath("//span[@class='menu-text' and contains(text(), ' Управление ')]")).Click();
        }
        public void OpenManageProjectPage()
        {
            if (driver.Url == baseURL + "mantisbt-2.25.3/manage_proj_page.php"
                && IsElementPresent(By.XPath("//button[@type='submit' and contains(text(), 'Создать новый проект')]")))
            {
                return;
            }
            driver.FindElements(By.CssSelector("ul.nav-tabs li"))[2].Click();
        }
    }
}
