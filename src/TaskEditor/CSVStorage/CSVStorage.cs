using System;
using System.Collections.Generic;

namespace CSVStorage
{
    public class CSVStorage
    {
        public static List<ICSVRow> ReadCSV(string fName, ICSVRow rowType)
        {
            List<ICSVRow> rows = new();

            rows.Add(rowType.Create(new List<string>() { "1", "Name", "Desc", "1704074481", "1704074481", "1", "todo" }));

            return rows;
        }

    }
}
