using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgmoEditor.ProjectEditors
{
    public interface IProjectChanger
    {
        string ErrorCheck();
        void LoadFromProject(Project project);
        void ApplyToProject(Project project);
    }
}
