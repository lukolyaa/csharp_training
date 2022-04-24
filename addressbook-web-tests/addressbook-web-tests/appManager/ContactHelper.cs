using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

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
            manager.Navigator.OpenHomePage();
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d =>
                driver.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        public void SelectGroupToAdd(string groupName)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(groupName);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public ContactHelper RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            SelectGroup(group.Name);
            SelectContact(contact.Id);
            SubmitContactRemoveFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        private ContactHelper SelectGroup(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
            return this;
        }

        private ContactHelper SubmitContactRemoveFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
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

        public ContactHelper Modify(ContactData oldContactData, ContactData newContactData)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(oldContactData.Id);
            FillContactForm(newContactData);
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
        public void Remove(ContactData contact)
        {
            manager.Navigator.OpenHomePage();
            SelectContact(contact.Id);
            RemoveContact();
            SubmitContactRemoval();
            manager.Navigator.OpenHomePage();
        }
        private List<ContactData> contactCache = null;
        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("[name=entry]"));
                foreach (IWebElement element in elements)
                {
                    string firstname = element.FindElement(By.XPath(".//td[3]")).Text;
                    string lastname = element.FindElement(By.XPath(".//td[2]")).Text;

                    contactCache.Add(new ContactData(firstname, lastname)
                    {
                        Id = element.FindElement(By.Name("selected[]")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactData>(contactCache);
        }
        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("[name=entry]")).Count;
        }

        public ContactHelper ContactExistenceCheck()
        {
            if (IsElementPresent(By.Name("selected[]")))
            {
                ContactData contact = new ContactData("Evgeniy", "Petrosyan");
                CreateContact(contact);          
            }
            return this;
        }

        public ContactHelper SubmitContactRemoval()
        {
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
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
            contactCache = null;
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int v, int p)
        {
            // driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + v + "]/td[" + p + "]/a/img")).Click();
            driver.FindElements(By.Name("entry"))[v].FindElements(By.TagName("td"))[p].FindElement(By.TagName("a")).Click();
            return this;
        }
        public ContactHelper InitContactModification(string id)
        {
            driver.FindElement(By.XPath("//a[@href='edit.php?id=" + id + "']")).Click();
            return this;
        }
        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));

            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            ContactData contact = new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones
            };
            return contact;

        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(index, 7);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            ContactData contact = new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email1 = email1,
                Email2 = email2,
                Email3 = email3
            };

            return contact;
        }

        public string ConvertContactInformationFromEditFormToString(ContactData contact)
        {
            string firstName = CheckNotNull("", contact.Firstname, " ");
            string lastName = contact.Lastname;
            string nameFragment = (firstName + lastName).Trim();
            nameFragment = CheckNotNull("", nameFragment, "\r\n");

            string address = CheckNotNull("", contact.Address, "\r\n");

            string homePhone = CheckNotNull("\r\n" + "H: ", contact.HomePhone, "\r\n");
            string mobilePhone = CheckNotNull("M: ", contact.MobilePhone, "\r\n");
            string workPhone = CheckNotNull("W: ", contact.WorkPhone, "\r\n");
            string phonesFragment = (homePhone + mobilePhone + workPhone).Trim();
            phonesFragment = CheckNotNull("\r\n", phonesFragment, "\r\n");

            string email = CheckNotNull("", contact.Email1, "\r\n");
            string email2 = CheckNotNull("", contact.Email2, "\r\n");
            string email3 = CheckNotNull("", contact.Email3, "\r\n");
            string emailsFragment = (email + email2 + email3).Trim();
            emailsFragment = CheckNotNull("\r\n", emailsFragment, "\r\n");

            return (nameFragment + address + phonesFragment + emailsFragment).Trim();
        }

        public static string CheckNotNull(string text_in, string text, string text_out)
        {
            if (text.Equals("") || text.Equals(null))
            {
                return "";
            }
            return text_in + text + text_out;
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

        public string GetContactInformationFromDetailsForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(index, 6);

            string allData = driver.FindElement(By.Id("content")).Text;
            return allData;
        }
    }
}
