using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Cf.Docs
{
    public enum DocsRoot
    {
        Assets,
        Project,
        PersistentData,
        StreamingAssetsPath,
    }
    
    // Assets
    //  * Editor : Project Folder / Assets /
    //  * Build  : Build Folder   / {Project Name}_Data / 

    // Project
    //  * Editor : Project Folder /
    //  * Build  : Build Folder   / {Project Name}_Data /  
    
    // PersistentData
    //  * Editor : Project Folder /
    //  * Build  : Build Folder   /  
    
    // StreamingAssetsPath
    //  * Editor : Project Folder /
    //  * Build  : Build Folder   /  
    
    public enum DocsExtend
    {
        Xml,
        Json,
        Txt,
    }

    public abstract class Docs
    {
        private readonly string _mDocsFolderPath;
        private readonly string _mDocsPath;

        private readonly bool _mIsCreateAuto;
        
        protected Docs(DocsRoot docsRoot, string[] subPathArr, string fileName, DocsExtend extend, bool isCreateAuto = true)
        {
            // file name
            if (string.IsNullOrEmpty(fileName))
            {
                Debug.LogError("[Docs] File Name Is Null Or Empty");
                return;
            }
            
            // option
            _mIsCreateAuto = isCreateAuto;

            // docs folder path combine
            // 1. root
            // 2. sub
            _mDocsFolderPath = docsRoot switch
            {
                DocsRoot.Project =>
#if UNITY_EDITOR
                    Application.dataPath[.."Assets".Length],  
#else
                    Application.dataPath[.."Application.productName".Length],
#endif
                DocsRoot.Assets => 
                    Application.dataPath,
                
                DocsRoot.PersistentData => 
                    Application.persistentDataPath,
                
                DocsRoot.StreamingAssetsPath => 
                    Application.streamingAssetsPath,
                
                _ => "",
            };
            
            if (subPathArr != null)
            {
                string subPath = subPathArr.Aggregate(Path.Combine);

                _mDocsFolderPath = Path.Combine(_mDocsFolderPath, subPath);
            }

            // file full name combine
            // 1. file name
            // 2. extend
            string docsFullName = $"{fileName}.{extend.ToString().ToLower()}";
            
            // result : docs path combine
            _mDocsPath = Path.Combine(_mDocsFolderPath, docsFullName);

            // file exist 
            if (File.Exists(_mDocsPath))
            {
                return;
            }

            if (!isCreateAuto)
            {
                Debug.LogError($"[Docs] Docs Path \"{_mDocsPath}\" Is Not Exist");
            }

            // create
            Create();
        }

        protected abstract string CreateDocsFile();
        
        private void Create()
        {
            if (!Directory.Exists(_mDocsFolderPath))
            {
                Directory.CreateDirectory(_mDocsFolderPath);
            }

            using FileStream stream = new FileStream(_mDocsPath, FileMode.Create, FileAccess.Write);
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.Write(CreateDocsFile());
                writer.Close();
            }
                
            stream.Close();
        }

        public virtual void Read()
        {
            
        }

        public virtual void Write()
        {
            
        }
    }
}