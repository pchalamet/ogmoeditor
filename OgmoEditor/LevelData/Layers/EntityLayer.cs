﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OgmoEditor.Definitions.LayerDefinitions;
using System.Xml;
using OgmoEditor.LevelEditors.LayerEditors;
using OgmoEditor.LevelData.Resizers;

namespace OgmoEditor.LevelData.Layers
{
    public class EntityLayer : Layer
    {
        public new EntityLayerDefinition Definition { get; private set; }
        public List<Entity> Entities { get; private set; }

        public EntityLayer(EntityLayerDefinition definition)
            : base(definition)
        {
            Definition = definition;

            Entities = new List<Entity>();
        }

        public override XmlElement GetXML(XmlDocument doc)
        {
            XmlElement xml = doc.CreateElement(Definition.Name);

            foreach (Entity e in Entities)
                xml.AppendChild(e.GetXML(doc));

            return xml;
        }

        public override void SetXML(XmlElement xml)
        {
            foreach (XmlElement e in xml.ChildNodes)
                Entities.Add(new Entity(e));
        }

        public override LayerEditor GetEditor(LevelEditors.LevelEditor editor)
        {
            return new EntityLayerEditor(editor, this);
        }

        public override Resizer GetResizer()
        {
            return null;
        }
    }
}
