using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RKSI_bot.WindowsInteractions
{
    internal class ExcelGroups
    {
        public string PathToExcel;

        public ExcelGroups()
        {
            PathToExcel = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\Groups.xlsx";
        }

        public ExcelGroups(string filePath)
        {
            PathToExcel = filePath;
        }

        public string[][][] GetDataExcel(bool isAllWorksheets, int numberWorksheets = 0)
        {
            using (var package = new ExcelPackage(OpenExcelFile()))
            {
                List<ExcelWorksheet> worksheetsList;
                var worksheets = package.Workbook.Worksheets;

                if (worksheets.Count == 0)
                    return null;

                if (isAllWorksheets)
                    worksheetsList = worksheets.ToList();
                else
                    worksheetsList = new List<ExcelWorksheet>() { worksheets[numberWorksheets.ToString()] };

                string[][][] excelData = new string[worksheetsList.Count][][];


                for (int sheet = 0; sheet < worksheetsList.Count; sheet++)
                {
                    excelData[sheet] = GetArrayMaxValue(worksheets[sheet].Cells);

                    for (int colum = 0; colum < excelData[sheet].Length; colum++)
                    {
                        for (int row = 0; row < excelData[sheet][colum].Length; row++)
                        {
                            //Console.WriteLine(worksheets[sheet].Cells[row + 1, colum + 1].Text);
                            excelData[sheet][colum][row] = worksheets[sheet].Cells[row + 1, colum + 1].Text.ToUpper().Trim();
                        }
                    }
                }
                return excelData;
            }
        }

        public void SetDataExcel(string[][][] dataArray)
        {
            using (var package = new ExcelPackage(OpenExcelFile()))
            {
                for (int sheet = 0; sheet < dataArray.Length; sheet++)
                {
                    if (!(package.Workbook.Worksheets[sheet].Name == (sheet + 1).ToString()))
                        package.Workbook.Worksheets.Add((sheet + 1).ToString());

                    for (int colum = 0; colum < dataArray[sheet].Length; colum++)
                    {
                        for (int row = 0; row < dataArray[sheet][colum].Length; row++)
                        {
                            package.Workbook.Worksheets[sheet].Cells[row + 1, colum + 1].Value = dataArray[sheet][colum][row].ToUpper().Trim();
                        }
                    }
                }
                package.Save();
            }
        }

        private FileInfo OpenExcelFile()
        {
            if (!(PathToExcel.Contains(".xls") || PathToExcel.Contains(".xlsx")))
                PathToExcel = PathToExcel + ".xlsx";

            var file = new FileInfo(PathToExcel);

            if (!file.Exists)
                file.Create().Close();

            CreateSheet();

            return file;
        }

        private void CreateSheet()
        {
            using (var package = new ExcelPackage(new FileInfo(PathToExcel)))
            {
                if(package.Workbook.Worksheets.Count == 0)
                {
                    package.Workbook.Worksheets.Add("1");
                }
                package.Save();
            }
        }
        private string[][] GetArrayMaxValue(ExcelRange cells)
        {
            string[][] arrayCells;
            int rows = cells.Rows;
            int columns = cells.Columns;

            for (int column = 1; column < columns; column++)
            {
                arrayCells = new string[column][];

                for (int row = 1; row < rows; row++)
                {
                    if (cells[row, column].Text == "")
                    {
                        Console.WriteLine(column + ": " + row);
                        arrayCells[column - 1] = new string[row - 1];
                        return arrayCells;
                    }
                }
            }

            return null;
        }

    }
}