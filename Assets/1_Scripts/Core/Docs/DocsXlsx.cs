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
    public class DocsXlsx<T> : Docs<T> where T : class, new()
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
            
            var wb = new XSSFWorkbook();
            var sheet = wb.CreateSheet("Sheet0");


            var rowHead = sheet.CreateRow(0);
            var rowHeadCursor = 0;
            
            rowHead.CreateCell(rowHeadCursor).SetCellValue("index");
            rowHeadCursor++;
            
            
            foreach (MemberInfo m in typeof(T).GetMembers().Where(m => m.MemberType == MemberTypes.Field))
            {
                var field = (FieldInfo)m;
                
                rowHead.CreateCell(rowHeadCursor).SetCellValue(field.Name);
                rowHeadCursor++;
            }
            
            
            // List<MemberInfo> memberList = typeof(T).GetMembers().Where(m => m.MemberType == MemberTypes.Field).ToList();
            //
            // IWorkbook wb = new XSSFWorkbook();
            // ISheet sheet = wb.CreateSheet("Sheet0");
            //
            // IRow headerRow = sheet.CreateRow(0);
            // headerRow.CreateCell(0).SetCellValue("index");
            // for (int i = 0; i < memberList.Count; i++)
            // {
            //     headerRow.CreateCell(i + 1).SetCellValue(memberList[i].Name);
            // }
         
            // IRow dataRow = sheet.CreateRow(1);
            // for (int i = 0; i < memberList.Count; i++)
            // {
            //     var value = ((FieldInfo)memberList[i]).GetValue(t);
            //
            //     switch (value)
            //     {
            //         case string strValue:
            //             dataRow.CreateCell(i + 1).SetCellValue(strValue);
            //             break;
            //         case int intValue:
            //             dataRow.CreateCell(i + 1).SetCellValue(intValue);
            //             break;
            //         case long longValue:
            //             dataRow.CreateCell(i + 1).SetCellValue(longValue);
            //             break;
            //         case bool boolValue:
            //             dataRow.CreateCell(i + 1).SetCellValue(boolValue);
            //             break;
            //         case float floatValue:
            //             dataRow.CreateCell(i + 1).SetCellValue(floatValue);
            //             break;
            //         default:
            //             dataRow.CreateCell(i + 1).SetCellValue(string.Empty);
            //             break;
            //     }
            // }
            
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
                var cell = valueRow.GetCell(i + 1);
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
                        var field = (FieldInfo)memberList[i];
                        
                        if (field.FieldType == typeof(int))
                        {
                            value = (int)cell.NumericCellValue;
                        }
                        else if (field.FieldType == typeof(long))
                        {
                            value = (long)cell.NumericCellValue;
                        }
                        else if (field.FieldType == typeof(float))
                        {
                            value = (float)cell.NumericCellValue;
                        }
                        else
                        {
                            value = cell.NumericCellValue;
                        }
                    }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (value == null)
                {
                    continue;
                }
                
                ((FieldInfo)memberList[i]).SetValue(t, value);
            }
            
            return t;
        }
    }
}
