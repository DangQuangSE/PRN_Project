using FManagement.Entities.QuangND.Entities;
using FManagement.Services.QuangND;
using FManagement.WorkerService.QuangND;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddHostedService<Worker>();
        builder.Services.AddWindowsService(options =>
        {
            options.ServiceName = "QuangND_SE1816";
        });
        builder.Services.AddSingleton<IProductPlanQuangNDService, ProductPlanQuangNDService>();
        var host = builder.Build();
        host.Run();
    }
}