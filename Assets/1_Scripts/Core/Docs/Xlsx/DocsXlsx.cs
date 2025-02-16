using System.IO;
using UnityEngine;

namespace Cf.Docs
{
    public class DocsXlsx<T> : Docs<T> where T : class, new()
    {
        public DocsXlsx(DocsRoot docsRoot, string[] subPathArr, string fileName, bool isCreateAuto = true) : base(docsRoot, subPathArr, fileName, DocsExtend.Xlsx, isCreateAuto)
        {
        }

        protected override string CreateDocsData(T t)
        {
            return "";
        }

        protected override T ReadDocsFile()
        {
            return new T();
        }
    }
}
