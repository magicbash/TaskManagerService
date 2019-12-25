using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Extensions;
using Microsoft.AspNetCore.Mvc;
using TaskManagerService.Business;
namespace TaskManagerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        
        [HttpGet("InProgress")]
        public async Task<IActionResult> GetStatisticAsync(DateTime dateTime, CancellationToken cancellationToken)
        {
            var result = await _statisticService.GetStatisticAsync(dateTime, cancellationToken);
            MemoryStream memoryStream = new MemoryStream();
            result.SaveAs((Stream) memoryStream);
            memoryStream.Seek(0L, SeekOrigin.Begin);
            return File(memoryStream, "application/octet-stream", "statistic.xlsx");
        }
    }
}