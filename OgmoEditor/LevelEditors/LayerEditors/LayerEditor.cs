using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace OgmoEditor.LevelEditors.LayerEditors
{
    public class LayerEditor
    {
        public Layer Layer { get; private set; }
        public LevelEditor LevelEditor { get; private set; }

        public LayerEditor(LevelEditor levelEditor, Layer layer)
        {
            LevelEditor = levelEditor;
            Layer = layer;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void OnKeyDown(KeyEventArgs e)
        {

        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {
            
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {

        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {

        }
    }
}
