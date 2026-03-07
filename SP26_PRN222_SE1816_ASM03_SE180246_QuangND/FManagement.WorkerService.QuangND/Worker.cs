using FManagement.Services.QuangND;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FManagement.WorkerService.QuangND
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IProductPlanQuangNDService _productPlanQuangNDService;
        public Worker(ILogger<Worker> logger, IProductPlanQuangNDService productionPlanService)
        {
            _logger = logger;
            _productPlanQuangNDService = productionPlanService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //if (_logger.IsEnabled(LogLevel.Information))
                //{
                //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //}
                await this.WriteToFile();
                await Task.Delay(5000, stoppingToken);
            }
        }
        protected async Task WriteToFile()
        {
            var items = await _productPlanQuangNDService.GetAllAsync();
            var opt = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var content = JsonSerializer.Serialize(items, opt);
            var path = @"D:\Datalog_SE1816.txt";
            using (var file = File.Open(path, FileMode.Append, FileAccess.Write))
            {
                using (var writer = new StreamWriter(file))
                {
                    await writer.WriteLineAsync(content);
                    await writer.FlushAsync();
                }
            }
        }
    }
}
