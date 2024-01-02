using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace CSVStorage
{
    public class CSVStorage
    {
        const string CSVSeparator = ";";
        public static List<ICSVRow> ReadCSV(string fName, ICSVRow rowType)
        {
            List<ICSVRow> rows = new();

            // source: https://stackoverflow.com/questions/5282999/reading-csv-file-and-storing-values-into-an-array

            using (TextFieldParser csvParser = new TextFieldParser(fName))
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
            StreamWriter sw = new StreamWriter(fName);

            string[] colNames = rowType.GetColumnHeaders();
            sw.WriteLine(String.Join(CSVSeparator, colNames));
            
            foreach (ICSVRow row in rows)
            {
                string sRow = String.Join(CSVSeparator, row.GetCols());
                sw.WriteLine(sRow);
            }
                
            sw.Close();
        }
    }
}
