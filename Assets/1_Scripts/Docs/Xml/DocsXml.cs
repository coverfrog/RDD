using UnityEngine;

namespace Cf.Docs
{
    public class DocsXml : Docs
    {
        public DocsXml(DocsRoot docsRoot, string[] subPathArr, string fileName, bool isCreateAuto = true) : base(docsRoot, subPathArr, fileName, DocsExtend.Xml, isCreateAuto)
        {
            
        }

        protected override string CreateDocsFile()
        {
            return "sssss";
        }
    }
}
