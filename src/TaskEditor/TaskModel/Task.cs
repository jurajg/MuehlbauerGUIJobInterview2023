using CSVStorageLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace TaskModelLib
{
    public class Task : ICSVRow, INotifyPropertyChanged
    {
        readonly string[] ColumnNames = { "Id", "Name", "Description", "StartDate", "DueDate", "ResponsiblePersonId", "Status" };

        public const int ColumnCount = 7;
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string ResponsiblePerson
        {
            get
            {
                Person person = parentTable?.parentModel?.personTable?.GetPersonById(ResponsiblePersonId);
                return person != null ? person.Name : "";
            }
        }
        private long _responsiblePersonId = 0;
        public long ResponsiblePersonId
        {
            get
            {
                return _responsiblePersonId;
            }
            set
            {
                _responsiblePersonId = value;
                NotifyPropertyChanged("ResponsiblePersonId");
                NotifyPropertyChanged("ResponsiblePerson");
            }
        }
        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public string Status { get; set; }

        public TaskTable parentTable;

        public event PropertyChangedEventHandler PropertyChanged;

        public Task()
        {
            StartDate = DateTime.Now;
            DueDate = StartDate.AddDays(1);
            parentTable = null;
        }

        ICSVRow ICSVRow.Create(string[] columns)
        {
            if(columns.Length != ColumnCount)
            {
                throw new ArgumentException($"Task constructor got wrong count of columns {columns.Length} instead of {ColumnCount}");
            }

            Task t = new();
            t.Id = long.Parse(columns[0]);
            t.Name = columns[1];
            t.Description = columns[2];
            t.StartDate = DateUtil.StringToLocalDate(columns[3]);
            t.DueDate = DateUtil.StringToLocalDate(columns[4]);
            t.ResponsiblePersonId = long.Parse(columns[5]);
            t.Status = columns[6];
            return t;
        }

        string[] ICSVRow.GetCols()
        {
            string[] cols = new string[ColumnCount];
            cols[0] = Id.ToString();
            cols[1] = Name;
            cols[2] = Description;
            cols[3] = DateUtil.DateToString(StartDate);
            cols[4] = DateUtil.DateToString(DueDate);
            cols[5] = ResponsiblePersonId.ToString();
            cols[6] = Status;
            return cols;
        }

        string[] ICSVRow.GetColumnHeaders()
        {
            return ColumnNames;
        }

    }
}
