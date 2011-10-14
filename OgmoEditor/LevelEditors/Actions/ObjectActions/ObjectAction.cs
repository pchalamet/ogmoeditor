using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Actions.ObjectActions
{
    public abstract class ObjectAction : OgmoAction
    {
        public ObjectLayer ObjectLayer { get; private set; }

        public ObjectAction(ObjectLayer objectLayer)
        {
            ObjectLayer = objectLayer;
        }
    }
}
