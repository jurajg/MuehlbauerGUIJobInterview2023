using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVStorage
{
    public interface ICSVRow
    {

        /// <summary>
        /// method takes in columns read from CSV row and returns new object
        /// </summary>
        /// <param name="columns"></param>
        /// <returns>created object containing parsed data</returns>
        public abstract ICSVRow Create(List<string> columns);

        /// <summary>
        /// creates string representation of data
        /// </summary>
        /// <returns>list of columns data converted to strings</returns>
        public abstract List<string> GetCols();
    }
}
