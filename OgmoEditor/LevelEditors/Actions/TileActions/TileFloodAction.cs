using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Drawing;

namespace OgmoEditor.LevelEditors.Actions.TileActions
{
    public class TileFloodAction : TileAction
    {
        private int setTo;
        private int was;
        private Point cell;

        private List<Point> changes;

        public TileFloodAction(TileLayer tileLayer, Point cell, int setTo)
            : base(tileLayer)
        {
            this.cell = cell;
            this.setTo = setTo;
        }

        public override void Do()
        {
            base.Do();

            was = TileLayer.Tiles[cell.X, cell.Y];
            changes = new List<Point>();
            flood(cell.X, cell.Y);
            TileLayer.TileCanvas.RefreshAll();
        }

        public override void Undo()
        {
            base.Undo();

            foreach (var p in changes)
                TileLayer.Tiles[p.X, p.Y] = was;
            TileLayer.TileCanvas.RefreshAll();
        }

        private void flood(int cellX, int cellY)
        {
            if (cellX < 0 || cellY < 0 || cellX > TileLayer.Tiles.GetLength(0) - 1 || cellY > TileLayer.Tiles.GetLength(1) - 1 || TileLayer.Tiles[cellX, cellY] != was)
                return;

            changes.Add(new Point(cellX, cellY));
            TileLayer.Tiles[cellX, cellY] = setTo;

            flood(cellX - 1, cellY);
            flood(cellX + 1, cellY);
            flood(cellX, cellY - 1);
            flood(cellX, cellY + 1);
        }
    }
}
