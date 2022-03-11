using Microsoft.Extensions.Options;
using TheBillboard.Abstract;
using TheBillboard.Gatweways;
using TheBillboard.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddOptions<AppOptions>()
    .Bind(builder.Configuration.GetSection("AppOptions"))
    .ValidateDataAnnotations();

builder.Services.AddSingleton<IGateway>(provider =>
    {
        var options = provider.GetRequiredService<IOptions<AppOptions>>();
        var logger = provider.GetRequiredService<ILogger<Gateway>>();
        
        return new Gateway(options, logger);
    }
);

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();