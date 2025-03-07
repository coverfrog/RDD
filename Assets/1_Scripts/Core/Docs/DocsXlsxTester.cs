using System;
using UnityEngine;

namespace Cf.Docs
{
    [Serializable]
    public class TestClass
    {
        public string str = "test";
        public int age = 2;
    }

    public class TestClassXlsx : DocsXlsx<TestClass>
    {
        public TestClassXlsx(DocsRoot docsRoot, string[] subPathArr, string fileName, bool isCreateAuto = true) : base(docsRoot, subPathArr, fileName, isCreateAuto)
        {
            
        }
    }

    public class DocsXlsxTester : MonoBehaviour
    {
        private void Start()
        {
            var x = new TestClassXlsx(DocsRoot.Assets, new[] { "__TEST___" }, "Simple");
        }
    }
}
