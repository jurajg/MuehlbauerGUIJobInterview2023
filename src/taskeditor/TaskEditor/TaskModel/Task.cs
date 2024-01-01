using CSVStorage;
using System;
using System.Collections.Generic;

namespace TaskModel
{
    public class Task : ICSVRow
    {
        public const int ColumnCount = 7;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int ResponsiblePersonId { get; set; }
        public string Status { get; set; }

        ICSVRow ICSVRow.Create(List<string> columns)
        {
            if(columns.Count!= ColumnCount)
            {
                throw new ArgumentException($"Task constructor got wrong count of columns {columns.Count} instead of {ColumnCount}");
            }

            Task t = new();
            t.Id = int.Parse(columns[0]);
            t.Name = columns[1];
            t.Description = columns[2];
            t.StartDate = DateUtil.StringToLocalDate(columns[3]);
            t.DueDate = DateUtil.StringToLocalDate(columns[4]);
            t.ResponsiblePersonId = int.Parse(columns[5]);
            t.Status = columns[6];
            return t;
        }

        List<string> ICSVRow.GetCols()
        {
            List<string> cols = new();
            cols.Add(Id.ToString());
            cols.Add(Name);
            cols.Add(Description);
            cols.Add(DateUtil.DateToString(StartDate));
            cols.Add(DateUtil.DateToString(DueDate));
            cols.Add(ResponsiblePersonId.ToString());
            cols.Add(Status);
            return cols;
        }
    }
}
