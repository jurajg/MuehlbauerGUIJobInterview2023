using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModelLib
{
    public abstract class Table
    {
        public TaskModel parentModel;
        public string TableName { get; }
        public Table(string sTableName)
        {
            TableName = sTableName;
        }
        public abstract void Load();
        public abstract void Save();
        public abstract void Clear();
        public abstract long CreateUniqueId();
    }
}
