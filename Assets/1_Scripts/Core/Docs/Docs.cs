using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Cf.Docs
{
    public enum DocsRoot
    {
        Assets = 0,
        Project,
        PersistentData,
        StreamingAssetsPath,
        
        EditorAssetsBuildProject = 100,
    }

    #region :: Path 

    // Assets
    //  * Editor : Project Folder / Assets /
    //  * Build  : Build Folder   / {Project Name}_Data / 

    // Project
    //  * Editor : Project Folder /
    //  * Build  : Build Folder   / {Project Name}_Data /  
    
    // PersistentData 
    //  * Window 
    //      * Editor : Pc User Dir / AppData / LocalLow / {Company Name} / {Product Name} /
    //      * Build  : Pc User Dir / AppData / LocalLow / {Company Name} / {Product Name} /
    //  * Mac 
    //      * Editor : Pc User Dir / Library / Cache / {Company Name.Product Name} /
    //      * Build  : Pc User Dir / Library / Cache / {Company Name.Product Name} /
    //  * Web
    //      * Disable
    //  * Ios
    //      * Editor : Pc User Dir / Library / Cache / {Company Name.Product Name} /
    //      * Build  : / var / mobile / Applications / {Program Id} / Documents / 
    //  * Android
    //      * Editor : Pc User Dir / AppData / LocalLow / {Company Name} / {Product Name} /
    //
    //      Build
    //          * External : / mnt / sdcard / Android / data / {Bundle Name} / files
    //          * Internal : / data / data / {Bundle Name} / files
    
    // StreamingAssetsPath
    //  * Editor : Project Folder / StreamingAssetsPath
    //  * Build  : Build Folder   / {Project Name}_Data / StreamingAssetsPath
    #endregion

    public enum DocsExtend
    {
        Xml,
        Json,
        Xlsx,
    }

    public abstract class Docs<T> where T : class, new()
    {
        protected readonly string DocsPath;
        protected readonly string DocsFolderPath;
        protected readonly string DocsFileName;
        private bool _mIsFileExist;

        protected Docs(DocsRoot docsRoot, string[] subPathArr, string fileName, DocsExtend extend, bool isCreateAuto = true)
        {
            // file name
            if (string.IsNullOrEmpty(fileName))
            {
                FilePathErrorLog(0);
                return;
            }

            DocsFileName = fileName;

            // docs folder path combine
            // 1. root
            // 2. sub
            DocsFolderPath = docsRoot switch
            {
                DocsRoot.Project =>
                    Directory.GetParent(Application.dataPath)?.FullName,
                
                DocsRoot.Assets => 
                    Application.dataPath,
                
                DocsRoot.PersistentData => 
                    Application.persistentDataPath,
                
                DocsRoot.StreamingAssetsPath => 
                    Application.streamingAssetsPath,
                
                
                DocsRoot.EditorAssetsBuildProject =>
#if UNITY_EDITOR
                    Application.dataPath,
#else
                    Directory.GetParent(Application.dataPath)?.FullName,
#endif
                
                _ => "",
            };

            if (DocsFolderPath == null)
            {
                FilePathErrorLog(2);
                return;
            }

            if (subPathArr != null)
            {
                string subPath = subPathArr.Aggregate(Path.Combine);

                DocsFolderPath = Path.Combine(DocsFolderPath, subPath);
            }

            // file full name combine
            // 1. file name
            // 2. extend
            string docsFullName = $"{fileName}.{extend.ToString().ToLower()}";
            
            // result : docs path combine
            DocsPath = Path.Combine(DocsFolderPath, docsFullName);

            // file exist 
            if (File.Exists(DocsPath))
            {
                _mIsFileExist = true;
                
                return;
            }

            if (!isCreateAuto)
            {
                FilePathErrorLog(1);
            }

            // create
            Create(null);

        }

        private void FilePathErrorLog(int errorCode)
        {
            string msg = errorCode switch
            {
                0 => $"[Docs] File Name Is Null Or Empty",
                1 => $"[Docs] Docs Path \"{DocsPath}\" Is Not Exist",
                2 => $"[Docs] Root Path Don't Find",
                _ => ""
            };
            
            Debug.LogError(msg);
        }

        protected abstract string CreateDocsData(T t);
        
        private void Create(T t)
        {
            if (!Directory.Exists(DocsFolderPath))
            {
                Directory.CreateDirectory(DocsFolderPath);
            }

            using FileStream stream = new FileStream(DocsPath, FileMode.Create, FileAccess.Write);
            using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.Write(CreateDocsData(t));
                writer.Close();
            }
                
            stream.Close();
            
            _mIsFileExist = true;
        }

        protected abstract T ReadDocsFile();

        public bool Read(out T docsStruct)
        {
            if (!_mIsFileExist)
            {
                FilePathErrorLog(1);

                docsStruct = new T();
                
                return false;
            }

            docsStruct = ReadDocsFile();
            
            return true;
        }

        public void Write(T t)
        {
            Create(t);
        }

        public void Delete()
        {
            if (!_mIsFileExist)
            {
                return;
            }

            File.Delete(DocsPath);
        }

        public bool IsExist()
        {
            return File.Exists(DocsPath);
        }
    }
}