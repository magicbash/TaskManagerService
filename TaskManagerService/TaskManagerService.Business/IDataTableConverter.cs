using System.Collections.Generic;
using System.Data;

namespace TaskManagerService.Business
{
    public interface IDataTableConverter
    {
        DataTable ConvertToDataTable<T>(IEnumerable<T> data);
    }
}