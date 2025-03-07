using System.IO;

namespace Cf.Docs
{
    public class DocsXlsx<T> :Docs<T> where T : class, new()
    {
        public DocsXlsx(DocsRoot docsRoot, string[] subPathArr, string fileName, DocsExtend extend, bool isCreateAuto = true) : base(docsRoot, subPathArr, fileName, extend, isCreateAuto)
        {
            
        }

        protected override string CreateDocsData(T t)
        {
            return "";
        }

        protected override T ReadDocsFile()
        {
            return null;
        }
    }
}
