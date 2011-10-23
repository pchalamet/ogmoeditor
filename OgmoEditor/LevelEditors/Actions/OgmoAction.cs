using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Actions
{
    public abstract class OgmoAction
    {
        public bool LevelWasChanged = true;

        public abstract void Do();
        public abstract void Undo();
    }
}
