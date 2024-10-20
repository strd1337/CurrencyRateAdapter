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

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CurrencyRateAdapter API V1"));
//}
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CurrencyRateAdapter API V1"));

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseExceptionHandler();

app.UseRateLimiter();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
