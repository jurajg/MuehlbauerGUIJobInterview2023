using CSVStorageLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModelLib
{
    public class Person : ICSVRow
    {
        readonly string[] ColumnNames = { "Id", "Name", "Birthday", "email"};

        public const int ColumnCount = 4;

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }

        public Person()
        {
            BirthDay = DateTime.Now;
        }

        ICSVRow ICSVRow.Create(string[] columns)
        {
            if (columns.Length != ColumnCount)
            {
                throw new ArgumentException($"Person constructor got wrong count of columns {columns.Length} instead of {ColumnCount}");
            }

            Person p = new();
            p.Id = long.Parse(columns[0]);
            p.Name = columns[1];
            p.BirthDay = DateUtil.StringToLocalDate(columns[2]);
            p.Email = columns[3];
            return p;
        }

        string[] ICSVRow.GetCols()
        {
            string[] cols = new string[ColumnCount];
            cols[0] = Id.ToString();
            cols[1] = Name;
            cols[2] = DateUtil.DateToString(BirthDay);
            cols[3] = Email;
            return cols;
        }

        string[] ICSVRow.GetColumnHeaders()
        {
            return ColumnNames;
        }
    }
}
