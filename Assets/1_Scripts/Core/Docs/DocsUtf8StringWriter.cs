using System.IO;
using System.Text;

public class DocsUtf8StringWriter : StringWriter
{
    // window string writer is utf-16, none modify public
    
    public override Encoding Encoding => new UTF8Encoding(false);
}
