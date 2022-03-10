using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBench
{

    public static class Extensions
    {
        /// <summary>
        /// Converts a List collection of SqlParameter to an observable collection using the constructor overload.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static ObservableCollection<SqlParameter> ToObservableCollection(this List<SqlParameter> parameters)
        {
            return new ObservableCollection<SqlParameter>(parameters);
        }
        /// <summary>
        /// Converts a List collection of strings to an observable collection using the constructor overload.
        /// </summary>
        /// <param name="stringList"></param>
        /// <returns></returns>
        public static ObservableCollection<string> ToObservableCollection(this List<string> stringList)
        {
            return new ObservableCollection<string>(stringList);
        }
        /// <summary>
        /// Provides an easy way to get the name of the file and nothing else.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns>Returns a string conatining the name of the file with the extension removed.</returns>
        public static string ShortName(this FileInfo fileInfo)
        {
            return fileInfo.Name.Replace(fileInfo.Extension, "");
        }
    }
}
