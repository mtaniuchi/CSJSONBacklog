/* See the file "LICENSE" for the full license governing this code. */

using System.Collections.Generic;
using CSJSONBacklog.Model.Issues;
using CSJSONBacklog.Model.Projects;
using CSJSONBacklog.Model.Space;

namespace CSJSONBacklog.API
{
    public interface IProjectAPI
    {
        #region Project
        /// <summary>
        /// Returns list of projects.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-projects"/>
        IEnumerable<Project> GetProjectList();

        /// <summary>
        /// Returns information about project.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-project"/>
        Project GetProject(string projectIdOrKey);

        Project AddProject(string projectIdOrKey);
        Project UpdateProject(string projectIdOrKey);
        Project DeleteProject(string projectIdOrKey);

        /// <summary>
        /// Returns list of Versions in the project.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-versions"/>
        IEnumerable<Version> GetVersionList(string projectIdOrKey);

        /// <summary>
        /// Returns list of Categories in the project.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-categories"/>
        IEnumerable<Category> GetCategoryList(string projectIdOrKey);

        #endregion Project



        #region Project User
        /// <summary>
        /// Returns list of project members.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-project-users"/>
        IEnumerable<User> GetProjectUserList(string projectIdOrKey);
        User AddProjectUser(string projectIdOrKey);
        User DeleteProjectUser(string projectIdOrKey);
        #endregion Project User


        #region Project Wiki
        /// <summary>
        /// Returns list of Wiki pages.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-wikis"/>
        IEnumerable<WikiPage> GetWikiPageList(string projectIdOrKey);

        /// <summary>
        /// Returns information about Wiki page.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-wiki"/>
        WikiPage GetWikiPage(string projectIdOrKey, int wikiId);

        #endregion Project Wiki

        #region Project Administrator
        /// <summary>
        /// Returns list of users who has Project Administrator role
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-project-adminnistrators"/>
        IEnumerable<User> GetProjectAdministratorList(string projectIdOrKey);
        User GetProjectAdministrator(string projectIdOrKey);
        User AddProjectAdministrator(string projectIdOrKey);
        User DeleteProjectAdministrator(string projectIdOrKey);
        #endregion Project Administrator



        #region misc
        /// <summary>
        /// Downloads project icon.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-project-icon"/>
        byte[] GetProjectIcon();
        /// <summary>
        /// Returns recent update in the project.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-project-activities"/>
        IEnumerable<Activitie> GetProjectRecentUpdates(string projectIdOrKey);
        /// <summary>
        /// Returns list of projects which the user viewed recently.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-user-recentlyviewedprojects"/>
        object GetListofRecentlyViewedProjects();
        /// <summary>
        /// Returns list of Custom Fields in the project.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-customfields"/>
        IEnumerable<CustomField> GetCustomFieldList(string projectIdOrKey);
        /// <summary>
        /// Returns list of Issue Types in the project.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-issuetypes"/>
        IEnumerable<IssueType> GetIssueTypeList(string projectIdOrKey);
        /// <summary>
        /// Returns information about project disk usage.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-project-diskusage"/>
        DiskUsage GetProjectDiskUsage(string projectIdOrKey);

        IEnumerable<Issue> GetListofRecentlyViewedIssues(string projectIdOrKey);
        IEnumerable<Project> GetListofRecentlyViewedProjects(string projectIdOrKey);
        IEnumerable<WikiPage> GetListofRecentlyViewedWikis(string projectIdOrKey);
        IEnumerable<Group> GetListofGroups(string projectIdOrKey);

        /// <summary>
        /// Returns list of Git repositories.
        /// </summary>
        /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/get-git-repositories"/>
        IEnumerable<GitRepository> GetListofGitRepositories(string projectIdOrKey);
        #endregion misc
    }
}
