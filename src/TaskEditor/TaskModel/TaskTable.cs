using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CSVStorageLib;

namespace TaskModelLib
{
    public class TaskTable : Table
    {
        public List<Task> data;

        public TaskTable() : base("task")
        {
            Clear();
        }

        public override void Clear()
        {
            data = new();
        }

        public Task CreateTask()
        {
            Task task = new();
            task.Id = CreateUniqueId();
            data.Add(task);
            return task;
        }

        public override long CreateUniqueId()
        {
            if (data.Count == 0) return 1;
            var maxId = data.Aggregate((agg, next) =>
                                       next.Id> agg.Id ? next : agg).Id;
            return maxId + 1;
        }

        public void DeleteTask(TaskModelLib.Task task)
        {
            data.Remove(task);
        }

        public void DeleteTaskById(long id)
        {
            data.RemoveAll(item => item.Id == id);
        }

        public override void Load()
        {
            string fPath = CSVStorage.GetTableFPath(TableName);

            if(!File.Exists(fPath))
            {
                Clear();
            } else
            {
                TaskModelLib.Task taskType = new();
                List<ICSVRow> result = CSVStorage.ReadCSV(fPath, taskType);
                data = new(result.Cast<TaskModelLib.Task>());

                foreach(Task task in data)
                {
                    task.parentTable = this;
                }
            }
        }

        public override void Save()
        {
            string fPath = CSVStorage.GetTableFPath(TableName);

            CSVStorage.CreateTablePath(TableName);

            List<ICSVRow> rows = new(data.Cast<ICSVRow>());
            TaskModelLib.Task taskType = new();
            CSVStorage.WriteCSV(fPath, rows, taskType);
        }
    }
}
