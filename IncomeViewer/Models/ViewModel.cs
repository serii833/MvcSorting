using System.Collections.Generic;
using System.Linq;

namespace IncomeViewer.Models
{
   

    public class Table<T>: List<T>
    {
        public Table(List<T> data)
        {
            AddRange(data);
        }

        public List<string> GetFields()
        {
            var propertyInfos = typeof(T).GetProperties();
            return propertyInfos.Select(a => a.Name).ToList();
        }

        public List<object> GetRowValues(T obj)
        {
            var propertyInfos = typeof (T).GetProperties();
            return propertyInfos.Select(a => a.GetValue(obj)).ToList();
        }
    }



    public class ViewModel<T>
    {
        public Table<T> Table { get; set; }

        public void AddTable(List<T> data)
        {
            Table = new Table<T>(data);
        }

    
    }

    public static class ViewModelExtensions
    {
        public static object GetValueByName<T>(this ViewModel<T> obj, string propertyName)
        {
            return typeof(T).GetProperty(propertyName).GetValue(obj, null);
        }
    }
}