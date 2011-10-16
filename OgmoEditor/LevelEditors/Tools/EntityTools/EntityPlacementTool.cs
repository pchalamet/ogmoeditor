using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OgmoEditor.LevelEditors.Tools.EntityTools
{
    using Rectangle = Microsoft.Xna.Framework.Rectangle;
    using OgmoEditor.LevelData.Layers;
    using OgmoEditor.LevelEditors.Actions.EntityActions;

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
                    LayerEditor.MouseSnapPosition.X - Ogmo.ObjectsWindow.CurrentEntity.Origin.X,
                    LayerEditor.MouseSnapPosition.Y - Ogmo.ObjectsWindow.CurrentEntity.Origin.Y,
                    Ogmo.ObjectsWindow.CurrentEntity.Size.Width,
                    Ogmo.ObjectsWindow.CurrentEntity.Size.Height);

                if (LevelEditor.Level.Bounds.Contains(Util.XNAToSystem(obj)))
                    content.DrawEntity(Ogmo.ObjectsWindow.CurrentEntity, obj, .5f);
            }
        }

        public override void OnMouseLeftClick(System.Drawing.Point location)
        {
            if (Ogmo.ObjectsWindow.CurrentEntity != null)
            {
                //Make sure the object will be within the level bounds
                System.Drawing.Rectangle obj = new System.Drawing.Rectangle(
                    LayerEditor.MouseSnapPosition.X - Ogmo.ObjectsWindow.CurrentEntity.Origin.X,
                    LayerEditor.MouseSnapPosition.Y - Ogmo.ObjectsWindow.CurrentEntity.Origin.Y,
                    Ogmo.ObjectsWindow.CurrentEntity.Size.Width,
                    Ogmo.ObjectsWindow.CurrentEntity.Size.Height);

                if (LevelEditor.Level.Bounds.Contains(obj))
                    LevelEditor.Perform(new EntityAddAction(LayerEditor.Layer, new Entity(Ogmo.ObjectsWindow.CurrentEntity, LayerEditor.MouseSnapPosition)));
            }
        }
    }
}
