using System.Collections.Generic;
using System.Threading;
using ClosedXML.Excel;
using TaskManagerService.Data.Enitites;

namespace TaskManagerService.Business
{
    public interface IStatisticReportGenerator<in TModel>
    {
        XLWorkbook GenerateReport(TModel data);
    }
}