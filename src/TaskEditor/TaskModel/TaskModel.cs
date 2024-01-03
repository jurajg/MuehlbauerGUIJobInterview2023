using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModelLib
{
    public class TaskModel
    {
        public TaskTable taskTable;
        public PersonTable personTable;

        public TaskModel()
        {
            taskTable = new();
            personTable = new();
        }

        public void Load()
        {
            taskTable.Load();
            personTable.Load();
        }

        public void Save()
        {
            taskTable.Save();
            personTable.Save();
        }

    }
}
