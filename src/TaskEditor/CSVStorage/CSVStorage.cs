using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;

namespace CSVStorageLib
{
    public class CSVStorage
    {
        const string CSVSeparator = ";";
        public static List<ICSVRow> ReadCSV(string fName, ICSVRow rowType)
        {
            List<ICSVRow> rows = new();

            // source: https://stackoverflow.com/questions/5282999/reading-csv-file-and-storing-values-into-an-array

            using (TextFieldParser csvParser = new(fName))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { CSVSeparator });
                csvParser.HasFieldsEnclosedInQuotes = false;

                // Skip the row with the column names
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();

                    rows.Add(rowType.Create(fields));
                }
            }

            return rows;
        }

        public static void WriteCSV(string fName, List<ICSVRow> rows, ICSVRow rowType)
        {
            StreamWriter sw = new(fName);

            string[] colNames = rowType.GetColumnHeaders();
            sw.WriteLine(String.Join(CSVSeparator, colNames));
            
            foreach (ICSVRow row in rows)
            {
                string sRow = String.Join(CSVSeparator, row.GetCols());
                sw.WriteLine(sRow);
            }
                
            sw.Close();
        }

        public static string GetStorageFolder()
        {
            // source: https://stackoverflow.com/questions/13762338/read-files-from-a-folder-present-in-project
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(assemblyFolder, @"data\");
        }

        public static string GetTableFPath(string tableName)
        {
            return Path.Combine(GetStorageFolder(), tableName + @".csv");
        }

        public static void CreatePath(string path)
        {
            // source: https://stackoverflow.com/questions/1321149/creating-a-file-that-the-path-does-not-exists
            FileInfo fileInfo = new(path);
            if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();
        }

        public static void CreateTablePath(string tableName)
        {
            CreatePath(GetTableFPath(tableName));
        }
    }
}
