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
            if (driver.Url == baseURL + "/my_view_page.php")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + "/my_view_page.php");
        }
    }
}
