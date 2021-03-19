using System.ComponentModel;

namespace UdemyIdentity.Enums
{
    public enum Permission
    {
        [Description("projects.view")]
        ProjectsViewPolicy,

        [Description("projects.create")]
        ProjectsCreate,

        [Description("projects.update")]
        ProjectsUpdate,
    }
}
