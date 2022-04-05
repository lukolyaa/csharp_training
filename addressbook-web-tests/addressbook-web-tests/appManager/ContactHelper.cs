using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    public class ContactHelper : HelperBase
    {

        public ContactHelper(ApplicationManager manager) 
            : base(manager)
        {
        }

        public ContactHelper CreateContact(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(int v, int p, ContactData newData)
        {
            InitContactModification(v, p);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int v)
        {

            SelectContact(v);
            RemoveContact();
            SubmitContactRemoval();
            return this;
        }

        public ContactHelper ContactExistenceCheck()
        {
            if (IsElementPresent(By.Id("3")) == false)
            {
                ContactData contact = new ContactData("Evgeniy");
                CreateContact(contact);          
            }
            return this;
        }

        public ContactHelper SubmitContactRemoval()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper SelectContact(int p)
        {
            driver.FindElement(By.XPath("//*[@id='maintable']/tbody/tr[" + p + "]/td/input")).Click();
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("middlename"), contact.Middlename);
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int v, int p)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + v + "]/td[" + p + "]/a/img")).Click();
            return this;
        }
    }
}
