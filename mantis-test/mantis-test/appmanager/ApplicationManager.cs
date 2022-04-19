using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using NUnit.Framework;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;
        public RegistrationHelper Registration { get; set; }
        public FtpHelper Ftp { get; set; }
        public JamesHelper James { get; set; }
        internal MailHelper Mail { get; set; }


        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();
        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/";
            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);
            
        }

        ~ApplicationManager()
        {
            {
                try
                {
                    driver.Quit();
                }
                catch (Exception)
                {
                    // Ignore errors if unable to close the browser
                }
            }
        }
        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = "http://localhost/mantisbt-2.25.2/login_page.php";
                app.Value = newInstance;
            }
            return app.Value;
        }
        public IWebDriver Driver
        {
            get
            {
                return driver;
            }

        }
    }
}