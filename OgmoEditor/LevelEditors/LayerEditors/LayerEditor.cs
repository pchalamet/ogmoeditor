using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace OgmoEditor.LevelEditors.LayerEditors
{
    using Point = System.Drawing.Point;
using OgmoEditor.LevelEditors.LayerEditors.Actions;  

    public class LayerEditor
    {
        public Layer Layer { get; private set; }
        public LevelEditor LevelEditor { get; private set; }
        public List<OgmoAction> ActionStack { get; private set; }

        public LayerEditor(LevelEditor levelEditor, Layer layer)
        {
            LevelEditor = levelEditor;
            Layer = layer;

            ActionStack = new List<OgmoAction>();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void OnKeyDown(Keys key)
        {

        }

        public virtual void OnKeyUp(Keys key)
        {

        }

        public virtual void OnMouseLeftClick(Point location)
        {
 
        }

        public virtual void OnMouseLeftDown(Point location)
        {
            
        }

        public virtual void OnMouseLeftUp(Point location)
        {

        }

        public virtual void OnMouseRightClick(Point location)
        {

        }

        public virtual void OnMouseRightDown(Point location)
        {

        }

        public virtual void OnMouseRightUp(Point location)
        {

        }

        public virtual void OnMouseMove(Point location)
        {

        }
    }
}
