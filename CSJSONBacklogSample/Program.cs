/* See the file "LICENSE" for the full license governing this code. */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CSJSONBacklog.Communicator;
using CSJSONBacklog.Model.Issues;
using CSJSONBacklog.Model.Space;

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
            string spaceName = Properties.Settings.Default.SpaceName;// must change!
            string apiKey = Properties.Settings.Default.APIKey;// must change!

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

            var users = getInfoSample.GetSpaceUsers().ToList();
            var userdic = new Dictionary<string, User>();
            var counter = new Dictionary<string, int>();

            // init user-project counter
            foreach (var user in users)
            {
                userdic.Add(user.UserId, user);
                counter.Add(user.UserId, 0);
            }

            // get project list
            var projects = getInfoSample.GetProjects().ToList();

            // check the user who does not belong to a project
            var projectCommunicator = new ProjectCommunicator(spaceName, apiKey);

            foreach (var project in projects)
            {
                var projectusers = projectCommunicator.GetProjectUserList(project.ProjectKey);

                foreach (var user in projectusers)
                {
                    int count;

                    if (counter.TryGetValue(user.UserId, out count))
                    {
                        count++;
                        counter[user.UserId] = count;
                    }
                }
            }

            Console.WriteLine("■プロジェクト未参加ユーザーの表示");
            foreach (var user in users)
            {
                int count;

                if (counter.TryGetValue(user.UserId, out count))
                {
                    if (count == 0)
                    {
                        User zerouser;

                        if (userdic.TryGetValue(user.UserId, out zerouser))
                        {
                            Console.WriteLine(zerouser);
                        }
                    }
                }
            }

            Console.WriteLine("■メンバーの居ないグループ");
            var groups = getInfoSample.GetGroups().ToList();

            foreach (var group in groups)
            {
                if (group.Members.Count == 0)
                {
                    Console.WriteLine(group);
                }
            }

            Console.WriteLine("■直近1ヶ月にアクセスの無いプロジェクト");
            var date = DateTime.Now.AddMonths(-1);

            foreach (var project in projects)
            {
                var activities = projectCommunicator.GetProjectRecentUpdates(project.ProjectKey);

                bool work = false;

                foreach (var activitie in activities)
                {
                    if (date < activitie.Created)
                    {
                        work = true;
                    }
                }
                if (!work)
                {
                    Console.WriteLine(project);

                    if (activities.Count() > 1)
                    {
                        Console.WriteLine(activities.First());
                    }
                }
            }

            Console.WriteLine("■1年間にアクセスの無いプロジェクト");
            date = DateTime.Now.AddYears(-1);

            foreach (var project in projects)
            {
                var activities = projectCommunicator.GetProjectRecentUpdates(project.ProjectKey);

                bool work = false;

                foreach (var activitie in activities)
                {
                    if (date < activitie.Created)
                    {
                        work = true;
                    }
                }
                if (!work)
                {
                    Console.WriteLine(project);

                    if (activities.Count() > 1)
                    {
                        Console.WriteLine(activities.First());
                    }
                }
            }

            //getInfoSample.PrintProjectDetails(projects);

            // print groups
            //var groups = getInfoSample.GetGroups().ToList();

            // print issues
            //foreach (var project in projects.Where(x => x.ProjectKey.Equals("SND")))
            //{
            //    var issues = getInfoSample.GetIssues(project);
            //    getInfoSample.PrintIssues(issues);
            //}
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
