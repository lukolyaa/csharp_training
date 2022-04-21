using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mantis_tests;
using NUnit.Framework;

namespace mantis_tests
{
    class ProjectRemovalTests : AuthTestBase
    {
        [Test]
        public void ProjectRemoveTest()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            if (app.Api.GetProjects(account).Count == 0)
            {
                ProjectData project = new ProjectData("project");
                app.Api.AddProject(account, project);
            }
            int num = 0;

            app.projectManagementHelper.CheckProjects();
            List<ProjectData> oldProjects = app.projectManagementHelper.GetProjectList();
            ProjectData toBeRemoved = oldProjects[num];
            app.projectManagementHelper.Remove(toBeRemoved);


            Assert.AreEqual(oldProjects.Count - 1, app.projectManagementHelper.GetProjectCount());
            List<ProjectData> newProjects = app.projectManagementHelper.GetProjectList();

            oldProjects.RemoveAt(num);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}