/* See the file "LICENSE" for the full license governing this code. */

namespace CSJSONBacklog.Model.Projects
{
    /// <summary>
    /// Project
    /// </summary>
    /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/add-project"/>
    public class Project
    {
        public int Id { get; set; }
        public string ProjectKey { get; set; }
        public string Name { get; set; }
        public bool ChartEnabled { get; set; }
        public bool SubTaskingEnabled { get; set; }
        public TextFormattingRule TextFormattingRule { get; set; }
        public bool Archived { get; set; }
        public int DisplayOrder { get; set; }

        public override string ToString()
        {
            return string.Format("Project: {0}({1}={2})", Name, ProjectKey, Id);
        }
    }

    /// <summary>
    /// TestFormattingRule
    /// </summary>
    /// <see cref="http://developer.nulab-inc.com/docs/backlog/api/2/add-project"/>
    public enum TextFormattingRule
    {
        Backlog,
        Markdown,
    }
}
