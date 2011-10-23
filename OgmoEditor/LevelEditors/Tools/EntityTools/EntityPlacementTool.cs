using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors.Actions.EntityActions;

namespace OgmoEditor.LevelEditors.Tools.EntityTools
{
    using Rectangle = Microsoft.Xna.Framework.Rectangle;  

    public class EntityPlacementTool : EntityTool
    {
        public EntityPlacementTool()
            : base("Create", "pencil.png", System.Windows.Forms.Keys.P)
        {

        }

        public override void Draw(Content content)
        {
            if (Ogmo.EntitiesWindow.CurrentEntity != null)
            {
                Rectangle obj = new Rectangle(
                    LayerEditor.MouseSnapPosition.X - Ogmo.EntitiesWindow.CurrentEntity.Origin.X,
                    LayerEditor.MouseSnapPosition.Y - Ogmo.EntitiesWindow.CurrentEntity.Origin.Y,
                    Ogmo.EntitiesWindow.CurrentEntity.Size.Width,
                    Ogmo.EntitiesWindow.CurrentEntity.Size.Height);

                content.DrawEntity(Ogmo.EntitiesWindow.CurrentEntity, LayerEditor.MouseSnapPosition, .5f);
            }
        }

        public override void OnMouseLeftClick(System.Drawing.Point location)
        {
            if (Ogmo.EntitiesWindow.CurrentEntity != null)
                LevelEditor.Perform(new EntityAddAction(LayerEditor.Layer, new Entity(Ogmo.EntitiesWindow.CurrentEntity, LayerEditor.MouseSnapPosition)));
        }
    }
}
