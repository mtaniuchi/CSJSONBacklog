/* See the file "LICENSE" for the full license governing this code. */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CSJSONBacklog.Communicator;
using CSJSONBacklog.Model.Issues;

namespace CSJSONBacklogSample
{
    /// <summary>
    /// Sample Program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        static void Main(string[] args)
        {
            string spaceName = "your-space-key";// must change!
            string apiKey = "your-api-key";// must change!

            // 1.get information
            EnforceGetSamples(spaceName, apiKey);

            // 2.update information
            //SampleOfUpdateIssue(spaceName, apiKey, "PROJ1");
        }


        /// <summary>
        /// Get API Samples
        /// </summary>
        private static void EnforceGetSamples(string spaceName, string apiKey)
        {
            var getInfoSample = new GetInfoSample(spaceName, apiKey);

            // print projects
            var projects = getInfoSample.GetProjects().ToList();
            getInfoSample.PrintProjectDetails(projects);

            // print issues
            foreach (var project in projects.Where(x => x.ProjectKey.Equals("SND")))
            {
                var issues = getInfoSample.GetIssues(project);
                getInfoSample.PrintIssues(issues);
            }
        }


        #region UPDATE

        private static void SampleOfUpdateIssue(string spaceName, string apiKey, string projectKey)
        {
            var projectCommunicator = new ProjectCommunicator(spaceName, apiKey);
            var projects = projectCommunicator.GetProjectList().ToList();
            var proj = projects.FirstOrDefault(x => x.ProjectKey.Equals(projectKey));

            var issueCommunicator = new IssueCommunicator(spaceName, apiKey);
            var count = issueCommunicator.GetIssuesCount(proj.Id);

            Debug.WriteLine("\t" + proj + " " + count);

            // issues in a project
            var param = new IssueQuery
            {
                ProjectIds = new List<int> { proj.Id },
                ParentChild = ParentChild.All,
                Offset = 0,
                Count = 100,// per 100 max
                Order = Order.asc,
                Sort = Sort.Created
            };
            var issue = issueCommunicator.GetIssues(param).FirstOrDefault();

            issue.description += " Update by api @" + DateTime.Now;
            issueCommunicator.UpdateIssue(issue);
        }

        #endregion

    }
}
