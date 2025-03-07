using System.IO;
using Newtonsoft.Json;

namespace Cf.Docs
{
    public class DocsJson<T> : Docs<T> where T : class, new()
    {
        public DocsJson(DocsRoot docsRoot, string[] subPathArr, string fileName, bool isCreateAuto = true) : base(docsRoot, subPathArr, fileName, DocsExtend.Json, isCreateAuto)
        {
        }

        protected override string CreateDocsData(T t)
        {
            return JsonConvert.SerializeObject(t, Formatting.Indented);
        }

        protected override T ReadDocsFile()
        {
            using StreamReader reader = new StreamReader(DocsPath);

            T t = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            
            reader.Close();

            return t;
        }
    }
}
