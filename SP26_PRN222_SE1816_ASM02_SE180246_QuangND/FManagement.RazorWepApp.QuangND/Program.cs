using FManagement.Services.QuangND;
using Microsoft.AspNetCore.Authentication.Cookies;
using FManagement.RazorWepApp.QuangND.Hubs;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddSignalR();
        builder.Services.AddScoped<IProductPlanQuangNDService, ProductPlanQuangNDService>();
        builder.Services.AddScoped<StoreOrderItemQuangNDService>();
        builder.Services.AddScoped<SystemAccountService>();
        builder.Services.AddScoped<ICentralKitchenService, CentralKitchenService>();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.AccessDeniedPath = "/Account/Forbidden";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();
        
        app.MapRazorPages().RequireAuthorization();
        
        app.MapHub<ProductionPlanHub>("/productionPlanHub");

        app.Run();
    }
}