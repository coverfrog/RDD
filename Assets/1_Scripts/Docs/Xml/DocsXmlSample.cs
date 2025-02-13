using Cf.Docs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cf.Docs
{
    public class DocsXmlSample : MonoBehaviour
    {
        private IEnumerator Start()
        {
            DocsXml<DocsXmlSampleStruct> xml0 = new DocsXml<DocsXmlSampleStruct>(
                DocsRoot.Project,
                null,
                "xml0");

            yield return new WaitForSeconds(1.0f);
            
            xml0.Read(out var t);
            
            Debug.Log("\n" + t.str + "\n" + t.i + "\n" + t.f + "\n" + t.b);

            yield return new WaitForSeconds(1.0f);

            t.str = "after";
            t.i = 100;
            t.f = 300.0f;
            t.b = false;
            
            Debug.Log("\n" + t.str + "\n" + t.i + "\n" + t.f + "\n" + t.b);
        }
    }
}
