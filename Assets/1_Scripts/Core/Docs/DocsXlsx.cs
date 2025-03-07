using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using UnityEngine;

namespace Cf.Docs
{
    public class DocsXlsx<T> :Docs<T> where T : class, new()
    {
        public DocsXlsx(DocsRoot docsRoot, string[] subPathArr, string fileName, bool isCreateAuto = true) : base(docsRoot, subPathArr, fileName, DocsExtend.Xlsx, isCreateAuto)
        {
            
        }

        protected override void Create(T t)
        {
            if (!Directory.Exists(DocsFolderPath))
            {
                Directory.CreateDirectory(DocsFolderPath);
            }
            
            List<MemberInfo> memberList = typeof(T).GetMembers().Where(m => m.MemberType == MemberTypes.Field).ToList();
            
            IWorkbook wb = new XSSFWorkbook();
            ISheet sheet = wb.CreateSheet("Sheet0");
    
            IRow headerRow = sheet.CreateRow(0);
            for (int i = 0; i < memberList.Count; i++)
            {
                headerRow.CreateCell(i).SetCellValue(memberList[i].Name);
            }
         
            IRow dataRow = sheet.CreateRow(1);
            for (int i = 0; i < memberList.Count; i++)
            {
                var value = ((FieldInfo)memberList[i]).GetValue(t);

                switch (value)
                {
                    case string strValue:
                        dataRow.CreateCell(i).SetCellValue(strValue);
                        break;
                    case int intValue:
                        dataRow.CreateCell(i).SetCellValue(intValue);
                        break;
                    case long longValue:
                        dataRow.CreateCell(i).SetCellValue(longValue);
                        break;
                    case bool boolValue:
                        dataRow.CreateCell(i).SetCellValue(boolValue);
                        break;
                    case float floatValue:
                        dataRow.CreateCell(i).SetCellValue(floatValue);
                        break;
                    default:
                        dataRow.CreateCell(i).SetCellValue(string.Empty);
                        break;
                }
            }
            
            using FileStream stream = new FileStream(DocsPath, FileMode.Create, FileAccess.Write);
            
            wb.Write(stream);
            wb.Close();
            
            stream.Close();
        }

        protected override string CreateDocsData(T t) { return null; }

        protected override T ReadDocsFile()
        {
            using FileStream fileStream = new FileStream(DocsPath, FileMode.Open, FileAccess.Read);

            T t = new T();
            List<MemberInfo> memberList = typeof(T).GetMembers().Where(m => m.MemberType == MemberTypes.Field).ToList();
            
            IWorkbook wb = new XSSFWorkbook(fileStream);
            ISheet sheet = wb.GetSheetAt(0);
            
            IRow headerRow = sheet.GetRow(0);
            IRow valueRow = sheet.GetRow(1);
            
            for (int i = 0; i < headerRow.LastCellNum; i++)
            {
                string header = headerRow.GetCell(i).StringCellValue;
                
                var cell = valueRow.GetCell(i);
                object value = null;

                switch (cell.CellType)
                {
                    case CellType.String:
                    {
                        value = cell.StringCellValue;
                    }
                        break;
                    case CellType.Boolean:
                    {
                        value = cell.BooleanCellValue;
                    }
                        break;
                    case CellType.Blank:
                    case CellType.Unknown:
                    case CellType.Formula:
                    case CellType.Error:
                        break;
                    case CellType.Numeric:
                    {
                        
                    }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (value == null)
                {
                    continue;
                }
                
                ((FieldInfo)memberList[0]).SetValue(t, value);
            }
            
            return t;
        }
    }
}
