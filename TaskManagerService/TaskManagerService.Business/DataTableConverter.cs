using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace TaskManagerService.Business
{
    public class DataTableConverter : IDataTableConverter
    {
        public DataTable ConvertToDataTable<T>(IEnumerable<T> data)

        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }
    }
}