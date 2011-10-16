using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Xml;
using OgmoEditor.LevelEditors.LayerEditors;

namespace OgmoEditor.LevelData.Layers
{
    public class EntityLayer : Layer
    {
        public new EntityLayerDefinition Definition { get; private set; }

        public EntityLayer(EntityLayerDefinition definition)
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
            return new EntityLayerEditor(editor, this);
        }
    }
}
