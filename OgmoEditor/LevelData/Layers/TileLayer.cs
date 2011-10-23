﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Xml;
using OgmoEditor.LevelEditors.LayerEditors;
using OgmoEditor.LevelData.Resizers;
using System.Drawing;

namespace OgmoEditor.LevelData.Layers
{
    public class TileLayer : Layer
    {
        public new TileLayerDefinition Definition { get; private set; }

        public TileLayer(TileLayerDefinition definition)
            : base(definition)
        {
            Definition = definition;
        }

        public override XmlElement GetXML(XmlDocument doc)
        {
            throw new NotImplementedException();
        }

        public override void SetXML(XmlElement xml)
        {
            throw new NotImplementedException();
        }

        public override LayerEditor GetEditor(LevelEditors.LevelEditor editor)
        {
            return new TileLayerEditor(editor, this);
        }

        public override Resizer GetResizer()
        {
            return null;
        }
    }
}
