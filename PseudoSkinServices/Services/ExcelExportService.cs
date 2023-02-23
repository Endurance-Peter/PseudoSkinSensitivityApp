using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinServices
{
    public class ExcelExportService : IExcelService
    {
        public ExcelExportService()
        {
            app = new Application();
            workbook = app.Workbooks.Add(Type.Missing);
        }
        public void ExportToExcel(string title, Dictionary<string,object> pseudoskinValues , Dictionary<string, double[]> results)
        {
            app = new Application();
            workbook = app.Workbooks.Add(Type.Missing);
            app.Visible = true;
            worksheet = (_Worksheet)workbook.Sheets["Pseudoskin Result"];
            worksheet = (_Worksheet)workbook.ActiveSheet;

            var topHeading = worksheet.Range[0, 6];
            topHeading.Merge();
            topHeading.Value2 = title;
            SetPseudoskinValues(pseudoskinValues);
            var count = pseudoskinValues.Count;
            SetResultTable(results, count);
        }

        private void SetResultTable(Dictionary<string, double[]> results, int count)
        {
            var col = 0;
            foreach (var result in results)
            {
                var row = count + 2;
                worksheet.Cells[row, col] = result.Key;
                foreach (var value in result.Value)
                {
                    row++;
                    worksheet.Cells[row, col] = value;
                }
                col++;
            }
        }

        private void SetPseudoskinValues(Dictionary<string, object> pseudoskinValues)
        {
            var row = 0; var col = 0;

            foreach (var pseudoskinValue in pseudoskinValues)
            {
                worksheet.Cells[row + 1, col] = pseudoskinValue.Key;
                worksheet.Cells[row + 1, col + 1] = pseudoskinValue.Value;

                row++;
                col++;
            }
        }

        private _Worksheet worksheet = null;
        private _Workbook workbook = null;
        private _Application app = null;
    }

    public interface IExcelService
    {
        public void ExportToExcel(string title, Dictionary<string, object> pseudoskinValues, Dictionary<string, double[]> results);
    }
}
