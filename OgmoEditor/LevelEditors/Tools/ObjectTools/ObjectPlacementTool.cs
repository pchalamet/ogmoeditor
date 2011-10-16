using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OgmoEditor.LevelEditors.Tools.ObjectTools
{
    using Rectangle = Microsoft.Xna.Framework.Rectangle;

    public class ObjectPlacementTool : ObjectTool
    {
        public ObjectPlacementTool()
            : base("Object Placement", "pencil.png", System.Windows.Forms.Keys.P)
        {

        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (Ogmo.ObjectsWindow.CurrentObject != null)
            {
                Rectangle obj = new Rectangle(
                    LevelEditor.LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].MouseSnapPosition.X - Ogmo.ObjectsWindow.CurrentObject.Origin.X,
                    LevelEditor.LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].MouseSnapPosition.Y - Ogmo.ObjectsWindow.CurrentObject.Origin.Y,
                    Ogmo.ObjectsWindow.CurrentObject.Size.Width,
                    Ogmo.ObjectsWindow.CurrentObject.Size.Height);

                if (LevelEditor.Level.Bounds.Contains(Util.XNAToSystem(obj)))
                    LevelEditor.Content.DrawObject(spriteBatch, Ogmo.ObjectsWindow.CurrentObject, obj, 1);
            }
        }
    }
}
