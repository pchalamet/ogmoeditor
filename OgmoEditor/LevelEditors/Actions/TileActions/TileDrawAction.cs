using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Drawing;

namespace OgmoEditor.LevelEditors.Actions.TileActions
{
    public class TileDrawAction : TileAction
    {
        private int setTo;
        private List<Point> draw;
        private List<int> was;

        public TileDrawAction(TileLayer tileLayer, Point at, int setTo)
            : base(tileLayer)
        {
            this.setTo = setTo;

            draw = new List<Point>();
            draw.Add(at);

            was = new List<int>();
        }

        public override void Do()
        {
            foreach (var at in draw)
            {
                was.Add(TileLayer.Tiles[at.X, at.Y]);
                TileLayer.Tiles[at.X, at.Y] = setTo;
            }

            TileLayer.RefreshTexture();
        }

        public override void Undo()
        {
            for (int i = 0; i < draw.Count; i++)
                TileLayer.Tiles[draw[i].X, draw[i].Y] = was[i];

            TileLayer.RefreshTexture();
        }

        public void DoAgain(Point add)
        {
            draw.Add(add);
            was.Add(TileLayer.Tiles[add.X, add.Y]);
            TileLayer.Tiles[add.X, add.Y] = setTo;

            TileLayer.RefreshTexture();
        }
    }
}
