﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using OgmoEditor.LevelEditors.ValueEditors;
using OgmoEditor.LevelData.Layers;

namespace OgmoEditor.Definitions.ValueDefinitions
{
    [XmlInclude(typeof(BoolValueDefinition))]
    [XmlInclude(typeof(EnumValueDefinition))]
    [XmlInclude(typeof(FloatValueDefinition))]
    [XmlInclude(typeof(IntValueDefinition))]
    [XmlInclude(typeof(StringValueDefinition))]
    [XmlInclude(typeof(ColorValueDefinition))]

    public class ValueDefinition
    {
        static public readonly List<Type> VALUE_TYPES = new List<Type>(new Type[] { typeof(IntValueDefinition), typeof(BoolValueDefinition), typeof(FloatValueDefinition), typeof(StringValueDefinition), typeof(EnumValueDefinition), typeof(ColorValueDefinition) });
        static public readonly List<string> VALUE_NAMES = new List<string>(new string[] { "Integer", "Boolean", "Float", "String", "Enum", "Color" });

        public enum NumberUITypes { Field, Slider };

        [XmlAttribute]
        public string Name;

        public ValueDefinition()
        {
            Name = "";
        }

        public virtual UserControl GetEditor()
        {
            throw new NotImplementedException();
        }

        public virtual ValueEditor GetInstanceEditor(Value instance, int x, int y)
        {
            throw new NotImplementedException();
        }

        public virtual ValueDefinition Clone()
        {
            throw new NotImplementedException();
        }

        public virtual string GetDefault()
        {
            throw new NotImplementedException();
        }
    }
}
