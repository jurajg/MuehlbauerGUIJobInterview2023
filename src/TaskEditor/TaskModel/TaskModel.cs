using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModelLib
{
    public class TaskModel
    {
        TaskTable taskTable;

        public TaskModel()
        {
            taskTable = new();
        }

        public void Load()
        {
            taskTable.Load();
        }

    }
}
