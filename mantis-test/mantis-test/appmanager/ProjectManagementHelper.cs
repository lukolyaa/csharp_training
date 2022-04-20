using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager) { }
        public void Remove(int v)
        {
            manager.menuManagamentHelper.OpenManageOverviewPage();
            manager.menuManagamentHelper.OpenManageProjectPage();
            SelectProject(0);
            RemoveProject();
            AssertRemovalProject();
        }
        public void Create(ProjectData project)
        {
            manager.menuManagamentHelper.OpenManageOverviewPage();
            manager.menuManagamentHelper.OpenManageProjectPage();
            InitNewProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
        }

        public void AssertRemovalProject()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }

        internal int GetProjectCount()
        {
            manager.menuManagamentHelper.OpenManageOverviewPage();
            manager.menuManagamentHelper.OpenManageProjectPage();
            IWebElement cell = driver.FindElements(By.CssSelector("div.table-responsive"))[0].FindElement(By.TagName("tbody"));
            return cell.FindElements(By.TagName("tr")).Count();
        }

        public void RemoveProject()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }

        public void SelectProject(int index)
        {
            IWebElement cell = driver.FindElements(By.CssSelector("div.table-responsive"))[0].FindElement(By.TagName("tbody"));
            cell.FindElements(By.TagName("tr"))[index].FindElements(By.TagName("td"))[0].FindElement(By.TagName("a")).Click();
        }

        public void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Добавить проект']")).Click();
        }

        public void InitNewProjectCreation()
        {
            driver.FindElement(By.XPath("//button[@type='submit' and contains(text(), 'Создать новый проект')]")).Click();
        }

        public void FillProjectForm(ProjectData project)
        {
            driver.FindElement(By.Name("name")).SendKeys(project.Name);
            driver.FindElement(By.Name("description")).SendKeys(project.Description);
        }

        public void CheckProjects()
        {
            manager.menuManagamentHelper.OpenManageOverviewPage();
            manager.menuManagamentHelper.OpenManageProjectPage();
            IWebElement cell = driver.FindElements(By.CssSelector("div.table-responsive"))[0].FindElement(By.TagName("tbody"));
            int count = cell.FindElements(By.TagName("tr")).Count();
            if (count == 0)
            {
                ProjectData project = new ProjectData("test")
                {
                    Description = "test"
                };
                Create(project);
            }
        }
        public List<ProjectData> GetProjectList()
        {
            List<ProjectData> projects = new List<ProjectData>();
            manager.menuManagamentHelper.OpenManageOverviewPage();
            manager.menuManagamentHelper.OpenManageProjectPage();

            IWebElement cell = driver.FindElements(By.CssSelector("div.table-responsive"))[0].FindElement(By.TagName("tbody"));
            ICollection<IWebElement> elements = cell.FindElements(By.TagName("tr"));

            foreach (IWebElement element in elements)
            {
                IList<IWebElement> cells = element.FindElements(By.TagName("td"));
                string name = cells.ElementAt(0).Text;
                ProjectData project = new ProjectData(name);
                projects.Add(project);
            }
            return projects;
        }

    }
}