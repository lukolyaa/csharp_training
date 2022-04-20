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
            app.projectManagementHelper.CheckProjects();
            List<ProjectData> oldProjects = app.projectManagementHelper.GetProjectList();
            app.projectManagementHelper.Remove(0);


            Assert.AreEqual(oldProjects.Count - 1, app.projectManagementHelper.GetProjectCount());
            List<ProjectData> newProjects = app.projectManagementHelper.GetProjectList();

            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}