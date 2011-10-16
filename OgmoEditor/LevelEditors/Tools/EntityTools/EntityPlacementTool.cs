using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OgmoEditor.LevelEditors.Tools.EntityTools
{
    using Rectangle = Microsoft.Xna.Framework.Rectangle;

    public class EntityPlacementTool : EntityTool
    {
        public EntityPlacementTool()
            : base("Object Placement", "pencil.png", System.Windows.Forms.Keys.P)
        {

        }

        public override void Draw(Content content)
        {
            if (Ogmo.ObjectsWindow.CurrentEntity != null)
            {
                Rectangle obj = new Rectangle(
                    LevelEditor.LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].MouseSnapPosition.X - Ogmo.ObjectsWindow.CurrentEntity.Origin.X,
                    LevelEditor.LayerEditors[Ogmo.LayersWindow.CurrentLayerIndex].MouseSnapPosition.Y - Ogmo.ObjectsWindow.CurrentEntity.Origin.Y,
                    Ogmo.ObjectsWindow.CurrentEntity.Size.Width,
                    Ogmo.ObjectsWindow.CurrentEntity.Size.Height);

                if (LevelEditor.Level.Bounds.Contains(Util.XNAToSystem(obj)))
                    content.DrawEntity(Ogmo.ObjectsWindow.CurrentEntity, obj, .5f);
            }
        }
    }
}
