using System;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace TaskManagerService.Business
{
    public interface IStatisticService
    {
        Task<XLWorkbook> GetStatisticAsync(DateTime time, CancellationToken cancellationToken);
    }
}