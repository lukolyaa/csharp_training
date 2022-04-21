using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mantis_tests;
using NUnit.Framework;

namespace mantis_tests
{
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]

        public void ProjectCreationTest()
        {
            ProjectData project = new ProjectData("project");
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            List<ProjectData> oldProjects = app.projectManagementHelper.GetProjectList();
            app.projectManagementHelper.Create(project);

            Assert.AreEqual(oldProjects.Count + 1, app.projectManagementHelper.GetProjectCount());

            List<ProjectData> newProjects = app.projectManagementHelper.GetProjectList();
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }

    }
}