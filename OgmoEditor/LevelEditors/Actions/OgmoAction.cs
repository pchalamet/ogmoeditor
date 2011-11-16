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

        /*
         *  Appending API. If an action is batched after another action of the same type and is appendable, 
         *  it will be appended to it instead of added to the stack.
         *  
         *  Important: Actions will only ever be appended to other actions of the same type.
         */
        public virtual bool Appendable { get { return false; } }
        public virtual void Append(OgmoAction action)
        {
            throw new NotImplementedException();
        }
    }
}
