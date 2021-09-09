using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace Дискриминантная_функция
{
    class Excel
    {
        private string path = "";
        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;

        public Excel(string path, int sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[sheet];
        }

        public double ReadCell(int i, int j)
        {
            i++;
            j++;

            if (ws.Cells[i, j].Value2 != null)
                return ws.Cells[i, j].Value2;
            else return 0;
        }
        public void WriteToCell(int i, int j, double k)
        {
            i++;
            j++;
            ws.Cells[i, j] = k;
        }
        public void Save()
        {
            wb.Save();
        }
        public void SaveToDirectory(string path)
        {
            wb.SaveAs(path);
        }
        public void Close()
        {
            wb.Close();
        }
        public double[,] ReadRange(int start_x, int start_y, int end_x, int end_y)
        {
            start_x++; start_y++; end_x++; end_y++;

            Range range = (Range)ws.Range[ws.Cells[start_x, start_y], ws.Cells[end_x, end_y]];

            object[,] holder = range.Value2;
            double[,] returnArray = new double[holder.GetLength(0), holder.GetLength(1)];

            for (int i = 1; i <= holder.GetLength(0); i++)
            {
                for (int j = 1; j <= holder.GetLength(1); j++)
                {
                    returnArray[i-1, j-1] = (double)holder[i, j];
                }
            }
            return returnArray;
        }
        public void WriteRange(int start_x, int start_y, int end_x, int end_y, double[,] WriteArr)
        {
            start_x++; start_y++; end_x++; end_y++;

            Range range = (Range)ws.Range[ws.Cells[start_x, start_y], ws.Cells[end_x, end_y]];

            range.Value2 = WriteArr;
        }
    }
}
