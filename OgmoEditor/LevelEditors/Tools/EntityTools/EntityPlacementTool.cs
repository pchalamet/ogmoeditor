using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OgmoEditor.LevelEditors.Tools.EntityTools
{
    using Rectangle = Microsoft.Xna.Framework.Rectangle;
    using OgmoEditor.LevelData.Layers;

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
                {
                    //Enforce entity limit defined by the entity definition
                    if (Ogmo.ObjectsWindow.CurrentEntity.Limit > 0 && LayerEditor.Layer.Entities.Count(e => e.Definition == Ogmo.ObjectsWindow.CurrentEntity) == Ogmo.ObjectsWindow.CurrentEntity.Limit)
                        LayerEditor.Layer.Entities.Remove(LayerEditor.Layer.Entities.Find(e => e.Definition == Ogmo.ObjectsWindow.CurrentEntity));

                    //Place the object
                    LayerEditor.Layer.Entities.Add(new Entity(Ogmo.ObjectsWindow.CurrentEntity, LayerEditor.MouseSnapPosition));
                }
            }
        }
    }
}
