using Microsoft.Extensions.Options;
using TheBillboard.MVC.Abstract;
using TheBillboard.MVC.Gateways;
using TheBillboard.MVC.Options;
using TheBillboard.MVC.Readers;
using TheBillboard.MVC.Writers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddOptions<AppOptions>()
    .Bind(builder.Configuration.GetSection("AppOptions"))
    .ValidateDataAnnotations();

// Add services to the container.
builder.Services
    .AddOptions<ConnectionStringOptions>()
    .Bind(builder.Configuration.GetSection("ConnectionStrings"))
    .ValidateDataAnnotations();

builder.Services.AddSingleton<IStudentGateway, StudentStudentGateway>();
builder.Services.AddSingleton<IMessageGateway, MessageGateway>();
builder.Services.AddSingleton<IAuthorGateway, AuthorGateway>();
builder.Services.AddSingleton<IReader, SqlReader>();
builder.Services.AddSingleton<IWriter, SqlWriter>();

// se vuoi farlo girare su postgre devi solo cambiare i servizi

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