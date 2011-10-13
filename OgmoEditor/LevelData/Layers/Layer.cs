using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using Microsoft.Xna.Framework.Graphics;

namespace OgmoEditor.LevelData.Layers
{
    public class Layer
    {
        public LayerDefinition Definition { get; private set; }

        public Layer(LayerDefinition definition)
        {
            Definition = definition;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
