using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Xml;
using OgmoEditor.LevelEditors.LayerEditors;

namespace OgmoEditor.LevelData.Layers
{
    public class ObjectLayer : Layer
    {
        public new ObjectLayerDefinition Definition { get; private set; }

        public ObjectLayer(ObjectLayerDefinition definition)
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
            return new ObjectLayerEditor(editor, this);
        }
    }
}
