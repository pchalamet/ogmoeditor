using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.LevelEditors.Actions.TileActions
{
    public class TilePasteSelectionAction : TileAction
    {
        private Rectangle area;
        private int[,] newData;

        private TileSelection oldSelection;

        public TilePasteSelectionAction(TileLayer tileLayer, Rectangle area, int[,] data)
            : base(tileLayer)
        {
            newData = data;
            this.area = area;
        }

        public override void Do()
        {
            base.Do();

            oldSelection = TileLayer.Selection;
            TileLayer.Selection = new TileSelection(TileLayer, area);
            TileLayer.Selection.SetUnderFromTiles();

            for (int i = 0; i < area.Width; i++)
                for (int j = 0; j < area.Height; j++)
                    TileLayer.Tiles[i + area.X, j + area.Y] = newData[i, j];

            TileLayer.TileCanvas.RefreshAll();
        }

        public override void Undo()
        {
            base.Undo();

            for (int i = 0; i < area.Width; i++)
                for (int j = 0; j < area.Height; j++)
                    TileLayer.Tiles[i + area.X, j + area.Y] = TileLayer.Selection.Under[i, j];

            TileLayer.Selection = oldSelection;
            TileLayer.TileCanvas.RefreshAll();
        }
    }
}
