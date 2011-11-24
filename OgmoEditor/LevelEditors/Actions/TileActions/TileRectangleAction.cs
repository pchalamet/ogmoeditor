using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Drawing;

namespace OgmoEditor.LevelEditors.Actions.TileActions
{
    public class TileRectangleAction : TileAction
    {
        private Rectangle rect;
        private int setTo;
        private int[,] was;

        public TileRectangleAction(TileLayer tileLayer, Rectangle rect, int setTo)
            : base(tileLayer)
        {
            this.rect = rect;
            this.setTo = setTo;
        }

        public override void Do()
        {
            base.Do();

            was = new int[rect.Width, rect.Height];
            for (int i = rect.X; i < rect.X + rect.Width; i++)
            {
                for (int j = rect.Y; j < rect.Y + rect.Height; j++)
                {
                    was[i - rect.X, j - rect.Y] = TileLayer.Tiles[i, j];
                    TileLayer.Tiles[i, j] = setTo;
                }
            }

            TileLayer.RefreshTexture();
        }

        public override void Undo()
        {
            base.Undo();

            for (int i = rect.X; i < rect.X + rect.Width; i++)
                for (int j = rect.Y; j < rect.Y + rect.Height; j++)
                    TileLayer.Tiles[i, j] = was[i - rect.X, j - rect.Y];

            was = null;
            TileLayer.RefreshTexture();
        }
    }
}
