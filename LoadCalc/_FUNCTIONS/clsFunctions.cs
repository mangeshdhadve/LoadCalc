using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace LoadCalc._FUNCTIONS
{
    class ClsFunctions
    {
        internal Dictionary<string, string[]> GetDictionaryFromExcel(ExcelWorksheet excelWorksheet, int ColumnCount)
        {
            Dictionary<string, string[]> retDictionary = new Dictionary<string, string[]>();
            int Row = 1; int Column = 1;
            int EmptyCount = 0;
            do
            {
                List<Object> Cells = new List<object>();
                int ColumnInc = 0;
                do
                {
                    Cells.Add(excelWorksheet.Cells[Row, Column + ColumnInc].Value);
                    ColumnInc++;
                } while (ColumnInc <= ColumnCount - 1 || Cells.Count != ColumnCount);

                if (Cells.Contains(null))
                {
                    Row++;
                    EmptyCount++;
                }
                else
                {
                    Row++;
                    EmptyCount = 0;
                    string Key = Cells[0].ToString();
                    List<string> lstValue = new List<string>();
                    for (int i = 0; i < Cells.Count; i++)
                    {
                        if (i != 0)
                            lstValue.Add(Cells[i].ToString());
                    }
                    string[] substr = lstValue.ToArray();
                    if (retDictionary.ContainsKey(Key))
                        retDictionary[Key] = substr;
                    else
                        retDictionary.Add(Key, substr);
                }
            } while (EmptyCount <= 1);

            return retDictionary;
        }

    }
}
