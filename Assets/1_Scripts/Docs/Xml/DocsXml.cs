using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Cf.Docs
{
    public class DocsXml<T> : Docs<T> where T : class, new()
    {
        public DocsXml(DocsRoot docsRoot, string[] subPathArr, string fileName, bool isCreateAuto = true) : base(docsRoot, subPathArr, fileName, DocsExtend.Xml, isCreateAuto)
        {
            
        }

        protected override string CreateDocsFile(T t)
        {
            // new struct
            t ??= new T();

            // serialize, settings
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = new UTF8Encoding(true), 
                OmitXmlDeclaration = false         
            };
            
            // writer
            using DocsUtf8StringWriter sw = new DocsUtf8StringWriter();
            using (XmlWriter writer = XmlWriter.Create(sw, settings))
            {
                serializer.Serialize(writer, t);
            }
            
            string xml = sw.ToString();
            
            sw.Close();

            return xml;
        }

        protected override T ReadDocsFile()
        {
            // reader
            using StreamReader reader = new StreamReader(DocsPath);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            
            return serializer.Deserialize(reader) as T;
        }
    }
}
