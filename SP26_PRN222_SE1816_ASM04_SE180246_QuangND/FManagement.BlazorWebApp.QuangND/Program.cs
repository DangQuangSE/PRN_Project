using FManagement.BlazorWebApp.QuangND.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();

builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Forbidden";
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddDbContextFactory<FManagement.Entities.QuangND.Entities.FranchiseManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<FManagement.Services.QuangND.IProductPlanQuangNDService, FManagement.Services.QuangND.ProductPlanQuangNDService>();
builder.Services.AddScoped<FManagement.Services.QuangND.ICentralKitchenService, FManagement.Services.QuangND.CentralKitchenService>();
builder.Services.AddScoped<FManagement.Services.QuangND.StoreOrderItemQuangNDService>();
builder.Services.AddScoped<FManagement.Repositories.QuangND.SystemAccountRepository>();
builder.Services.AddScoped<FManagement.Services.QuangND.SystemAccountService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorPages();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
