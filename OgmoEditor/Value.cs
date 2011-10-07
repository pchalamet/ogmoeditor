using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace OgmoEditor
{
    public class Value
    {
        string Name;
        string Val;

        public Value()
            : this("", "")
        {
        }

        public Value(string name, string val)
        {
            Name = name;
            Val = val;
        }
    }
}
