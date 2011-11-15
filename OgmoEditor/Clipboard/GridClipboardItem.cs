using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.LevelData.Layers;
using OgmoEditor.LevelEditors;
using OgmoEditor.LevelEditors.Actions.GridActions;

namespace OgmoEditor.Clipboard
{
    public class GridClipboardItem : ClipboardItem
    {
        private GridSelection selection;

        public GridClipboardItem(GridSelection selection)
            : base()
        {
            this.selection = new GridSelection(selection);
        }

        public override bool CanPaste(Layer layer)
        {
            return layer is GridLayer;
        }

        public override void Paste(LevelEditor editor, Layer layer)
        {
            editor.StartBatch();
            if ((layer as GridLayer).Selection != null)
                editor.BatchPerform(new GridClearSelectionAction(layer as GridLayer));
            editor.BatchPerform(new GridSetSelectionAction(layer as GridLayer, new GridSelection(selection)));
            editor.EndBatch();
        }
    }
}
