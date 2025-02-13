using System;
using System.Xml.Serialization;
using UnityEngine;

namespace Cf.Docs
{
    [Serializable, XmlRoot("Root")]
    public class DocsXmlSampleStruct
    {
        public string str = "before";
        public int i = 1;
        public float f = 3.456789f;
        public bool b = true;
    }
}