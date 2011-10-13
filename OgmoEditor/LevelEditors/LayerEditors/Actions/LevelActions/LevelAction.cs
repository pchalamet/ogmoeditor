using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgmoEditor.LevelEditors.LayerEditors.Actions.LevelActions
{
    public abstract class LevelAction : OgmoAction
    {
        public LevelEditor LevelEditor { get; private set; }

        public LevelAction(LevelEditor levelEditor)
        {
            LevelEditor = levelEditor;
        }
    }
}
