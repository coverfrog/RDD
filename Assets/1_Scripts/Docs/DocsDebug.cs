using Cf.Docs;
using UnityEngine;

namespace Cf
{
    public class DocsDebug : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            DocsXml xml0 = new DocsXml(DocsRoot.Assets, new []{"Test","as", "Asdf"}, "xml0");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
