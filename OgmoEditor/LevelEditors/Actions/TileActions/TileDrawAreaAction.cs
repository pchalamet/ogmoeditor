using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using System.Drawing;

namespace OgmoEditor.LevelEditors.Actions.TileActions
{
    public class TileDrawAreaAction : TileAction
    {
        private int[] setTo;
        private Size size;
        private List<TileDrawAction> actions = new List<TileDrawAction>();
        private List<TileDrawAction> undo = new List<TileDrawAction>();

        public TileDrawAreaAction(TileLayer tileLayer, Point at, Size size, int[] setTo)
            : base(tileLayer)
        {
            this.size = size;
            this.setTo = setTo;

            this.CalculateForLocation(at);
        }

        private void CalculateForLocation(Point at)
        {
            this.actions.Clear();
            int i = 0;
            for (int y = 0; y < size.Height; y += 1)
                for (int x = 0; x < size.Width; x += 1)
                {
                    this.actions.Add(new TileDrawAction(this.TileLayer, new Point(at.X + x, at.Y + y), setTo[i]));
                    i += 1;
                }
        }

        public override void Do()
        {
            foreach (TileDrawAction act in actions)
            {
                act.Do();
                undo.Add(act);
            }
        }

        public override void Undo()
        {
            List<TileDrawAction> copy = undo.ToList();
            copy.Reverse();
            foreach (TileDrawAction act in copy)
                act.Undo();
        }

        public void DoAgain(Point at)
        {
            this.CalculateForLocation(at);
            this.Do();
        }
    }
}
