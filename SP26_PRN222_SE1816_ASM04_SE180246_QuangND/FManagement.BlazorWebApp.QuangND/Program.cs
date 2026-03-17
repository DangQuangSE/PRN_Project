using FManagement.BlazorWebApp.QuangND.Components;
using FManagement.Services.QuangND;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // Add Authentication
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.AccessDeniedPath = "/forbidden";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
            });

        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = null;
        });

        // Add HttpContextAccessor for AuthenticationStateProvider
        builder.Services.AddHttpContextAccessor();

        // Add custom AuthenticationStateProvider
        builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

        builder.Services.AddScoped<IProductPlanQuangNDService, ProductPlanQuangNDService>();
        builder.Services.AddScoped<StoreOrderItemQuangNDService>();
        builder.Services.AddScoped<SystemAccountService>();
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            app.UseMigrationsEndPoint();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAntiforgery();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}