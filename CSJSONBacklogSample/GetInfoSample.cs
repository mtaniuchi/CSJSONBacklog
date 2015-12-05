/* See the file "LICENSE" for the full license governing this code. */

using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using CSJSONBacklog.Communicator;
using CSJSONBacklog.Model.Issues;
using CSJSONBacklog.Model.Projects;
using CSJSONBacklog.Model.Space;

namespace CSJSONBacklogSample
{
    internal class GetInfoSample
    {
        private readonly string _spaceName;
        private readonly string _apiKey;

        public GetInfoSample(string spaceName, string apiKey)
        {
            _spaceName = spaceName;
            _apiKey = apiKey;
        }


        /// <summary>
        /// get all project info in space
        /// </summary>
        public IEnumerable<Project> GetProjects()
        {
            // project list
            var projectCommunicator = new ProjectCommunicator(_spaceName, _apiKey);
            var projects = projectCommunicator.GetProjectList().ToList();
            return projects;
        }

        /// <summary>
        /// get all group info in space
        /// </summary>
        public IEnumerable<Group> GetGroups()
        {
            var spaceCommunicator = new SpaceCommunicator(_spaceName, _apiKey);
            var groups = spaceCommunicator.GetGroupList().ToList();
            return groups;
        }

        /// <summary>
        /// get all project info in space
        /// </summary>
        public IEnumerable<Issue> GetIssues(Project project)
        {
            var issueCommunicator = new IssueCommunicator(_spaceName, _apiKey);
            var count = issueCommunicator.GetIssuesCount(project.Id);

            Debug.WriteLine(project + " " + count);

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

            var result = new List<Issue>();
            for (param.Offset = 0; param.Offset < count; param.Offset += 100)
            {
                var issues = issueCommunicator.GetIssues(param).ToList();
                if (issues.Any()) { result.AddRange(issues); }
            }

#if false // get single
            if (result.Any())
            {
                var issue = issueCommunicator.GetIssue(result.First().id.ToString(CultureInfo.InvariantCulture));
                Debug.WriteLine("\t\t" + issue);
                issue = issueCommunicator.GetIssue(result.First().issueKey);
                Debug.WriteLine("\t\t" + issue);
            }
#endif

            return result;
        }


        public void PrintProjectDetails(IEnumerable<Project> projects)
        {
            var projectCommunicator = new ProjectCommunicator(_spaceName, _apiKey);
            foreach (var project in projects)
            {
                Debug.WriteLine(project);

                // disk usage
                var usage = projectCommunicator.GetProjectDiskUsage(project.ProjectKey);
                Debug.WriteLine("\t" + usage);

                // project user
                var users = projectCommunicator.GetProjectUserList(project.ProjectKey);
                foreach (var user in users)
                {
                    Debug.WriteLine("\t" + user);
                }

                var admins = projectCommunicator.GetProjectAdministratorList(project.ProjectKey);
                foreach (var user in admins)
                {
                    Debug.WriteLine("\t" + user);
                }

                // issue types
                var issueTypes = projectCommunicator.GetIssueTypeList(project.ProjectKey);
                foreach (var issueType in issueTypes)
                {
                    Debug.WriteLine("\t" + issueType);
                }

                // categories
                var categories = projectCommunicator.GetCategoryList(project.ProjectKey);
                foreach (var category in categories)
                {
                    Debug.WriteLine("\t" + category);
                }

                // version
                var versions = projectCommunicator.GetVersionList(project.ProjectKey);
                foreach (var version in versions)
                {
                    Debug.WriteLine("\t" + version);
                }

                // custom fields
                var customFieldList = projectCommunicator.GetCustomFieldList(project.ProjectKey);
                foreach (var customField in customFieldList)
                {
                    Debug.WriteLine("\t" + customField);
                }

#if false // TODO:{"errors":[{"message":"Authentication failure.","code":11,"moreInfo":""}]}
                // git repositories
                var repos = projectCommunicator.GetListofGitRepositories(project.ProjectKey);
                foreach (var gitRepository in repos)
                {
                    Debug.WriteLine("\t" + gitRepository);
                }
#endif
#if false // get single
                var oneProj = projectCommunicator.GetProject(project.ProjectKey);
                Debug.WriteLine("\t\t" + oneProj);
#endif
            }
        }

        public void PrintIssues(IEnumerable<Issue> issues)
        {
            if (issues == null || !issues.Any()) { return; }
            
            foreach (var issue in issues)
            {
                Debug.WriteLine("\t" + issue);
            }
        }
    }
}
