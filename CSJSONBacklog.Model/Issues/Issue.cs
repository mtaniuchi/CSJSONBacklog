﻿/* See the file "LICENSE" for the full license governing this code. */

using System;
using System.Collections.Generic;
using CSJSONBacklog.Model.Attributes;
using CSJSONBacklog.Model.Projects;
using CSJSONBacklog.Model.Space;

namespace CSJSONBacklog.Model.Issues
{
    /// <summary>
    /// Issue
    /// </summary>
    /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/add-issue"/>
    public class Issue
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public string issueKey { get; set; }
        public int keyId { get; set; }
        public IssueType issueType { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
        public Resolution resolution { get; set; }
        public Priority priority { get; set; }
        public Status status { get; set; }
        public User assignee { get; set; }
        public List<Category> category { get; set; }
        public List<object> versions { get; set; }
        public List<object> milestone { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? dueDate { get; set; }
        public Decimal? estimatedHours { get; set; }
        public Decimal? actualHours { get; set; }
        public int? parentIssueId { get; set; }
        public User createdUser { get; set; }
        public string created { get; set; }
        public User updatedUser { get; set; }
        public string updated { get; set; }
        public List<CustomField> customFields { get; set; }
        public List<Attachment> attachments { get; set; }
        public List<SharedFile> sharedFiles { get; set; }
        public List<Star> stars { get; set; }
        
        public override string ToString()
        {
            return string.Format("Issue: {0} {1}({2})", issueKey, summary, status);
        }


        /// <summary>
        /// return properties for Patch
        /// </summary>
        /// <returns></returns>
        [PatchPropertyNamesMethod]
        public static IEnumerable<string> PatchPropertyNames()
        {
            return new List<string>
            {
                @"summary",
                @"description"
            };
        }
    }
    
    public class IssueType
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public int displayOrder { get; set; }

        public override string ToString()
        {
            return string.Format("IssueType: {0}({1})", name, id);
        }
    }

    public class Resolution
    {
        public int id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return string.Format("Resolution: {0}", name);
        }
    }

    public class Priority
    {
        public int id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return string.Format("Priority: {0}", name);
        }
    }

    public class Status
    {
        public int id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return string.Format("Status: {0}", name);
        }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public int displayOrder { get; set; }

        public override string ToString()
        {
            return string.Format("Category: {0}", name);
        }
    }

    public class Milestone
    {
        public int id { get; set; }
        public int projectId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? releaseDueDate { get; set; }
        public bool archived { get; set; }
        public int displayOrder { get; set; }

        public override string ToString()
        {
            return string.Format("Milestone: {0}", name);
        }
    }

    public class Attachment
    {
        public int id { get; set; }
        public string name { get; set; }
        public long size { get; set; }

        public override string ToString()
        {
            return string.Format("Attachment: {0}", name);
        }
    }

    public class SharedFile
    {
        public int id { get; set; }
        public string type { get; set; }
        public string dir { get; set; }
        public string name { get; set; }
        public int size { get; set; }
        public User createdUser { get; set; }
        public DateTime? created { get; set; }
        public User UpdatedUser { get; set; }
        public DateTime? Updated { get; set; }

        public override string ToString()
        {
            return string.Format("SharedFile: {0}", name);
        }
    }

    public class Star
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public User Presenter { get; set; }
        public DateTime? Created { get; set; }

        public override string ToString()
        {
            return string.Format("Star: {0}(Presenter: {1})", Title, Presenter);
        }
    }



    public class CountValue
    {
        public int Count { get; set; }
    }
}
