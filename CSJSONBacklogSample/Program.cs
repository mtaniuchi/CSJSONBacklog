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
            string spaceKey = "your-space-key";// must change!
            string apiKey = "your-api-key";// must change!

            // 1.get information
            EnforceGetSamples(spaceKey, apiKey);

            // 2.update information
            SampleOfUpdateIssue(spaceKey, apiKey);
        }


        /// <summary>
        /// Get API Samples
        /// </summary>
        private static void EnforceGetSamples(string spaceKey, string apiKey)
        {
            var getInfoSample = new GetInfoSample(spaceKey, apiKey);

            // print projects
            var projects = getInfoSample.GetProjects().ToList();
            getInfoSample.PrintProjectDetails(projects);

            // print issues
            foreach (var project in projects)
            {
                var issues = getInfoSample.GetIssues(project);
                getInfoSample.PrintIssues(issues);
            }
        }


        #region UPDATE

        private static void SampleOfUpdateIssue(string spaceName, string apiKey)
        {
            var projectCommunicator = new ProjectCommunicator(spaceName, apiKey);
            var project = projectCommunicator.GetProjectList().FirstOrDefault();

            var issueCommunicator = new IssueCommunicator(spaceName, apiKey);
            var count = issueCommunicator.GetIssuesCount(project.Id);

            Debug.WriteLine("\t" + project + " " + count);

            // issues in a project
            var param = new IssueQuery
            {
                ProjectIds = new List<int> { project.Id },
                ParentChild = ParentChild.All,
                Offset = 0,
                Count = 100,// per 100 max
                Order = Order.asc,
                Sort = Sort.Created
            };
            var issue = issueCommunicator.GetIssues(param).FirstOrDefault();

            issue.description += " / Update by api-test @" + DateTime.Now;
            issueCommunicator.UpdateIssue(issue);
        }

        #endregion

    }
}
