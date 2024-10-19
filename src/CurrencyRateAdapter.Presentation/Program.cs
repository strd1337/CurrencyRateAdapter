using CurrencyRateAdapter.Presentation;
using CurrencyRateAdapter.Application;
using CurrencyRateAdapter.Infrastructure;
using CurrencyRateAdapter.Adapter;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddAdapter(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseExceptionHandler();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
