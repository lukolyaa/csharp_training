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
        public static IEnumerable<ProjectData> RandomProjectDataProvider()
        {
            List<ProjectData> contacts = new List<ProjectData>();
            for (int i = 0; i < 1; i++)
            {
                contacts.Add(new ProjectData(GenerateRandomString(15))
                {
                    Description = GenerateRandomString(30)
                });
            }

            return contacts;
        }
        [Test, TestCaseSource("RandomProjectDataProvider")]

        public void ProjectCreationTest(ProjectData project)
        {
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