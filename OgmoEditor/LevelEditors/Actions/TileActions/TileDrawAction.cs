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
        private Point at;
        private int setTo;
        private int was;

        public TileDrawAction(TileLayer tileLayer, Point at, int setTo)
            : base(tileLayer)
        {
            this.at = at;
            this.setTo = setTo;
        }

        public override void Do()
        {
            was = TileLayer.Tiles[at.X, at.Y];
            TileLayer.Tiles[at.X, at.Y] = setTo;
        }

        public override void Undo()
        {
            TileLayer.Tiles[at.X, at.Y] = was;
        }
    }
}
