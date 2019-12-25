using ClosedXML.Excel;
using TaskManagerService.Data.Models;

namespace TaskManagerService.Business
{
    public class ExcelReportGenerator : IStatisticReportGenerator<StatisticReportModel>
    {
        private readonly IDataTableConverter _dataTableConverter;

        public ExcelReportGenerator(IDataTableConverter dataTableConverter)
        {
            _dataTableConverter = dataTableConverter;
        }
        
        public XLWorkbook GenerateReport(StatisticReportModel data)
        {
            XLWorkbook wb = new XLWorkbook();

            wb.Worksheets.Add(_dataTableConverter.ConvertToDataTable(data.Projects), "Projects");
            wb.Worksheets.Add(_dataTableConverter.ConvertToDataTable(data.Tasks), "Tasks");

            return wb;
        }
    }
}