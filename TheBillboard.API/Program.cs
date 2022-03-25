using TheBillboard.API.Abstract;
using TheBillboard.API.Options;
using TheBillboard.API.Readers;
using TheBillboard.API.Repositories;
using TheBillboard.API.Writers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddOptions<ConnectionStringOptions>()
    .Bind(builder.Configuration.GetSection("ConnectionStrings"))
    .ValidateDataAnnotations();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMessageRepository, MessageRepository>();
builder.Services.AddSingleton<IAuthorRepository, AuthorRepository>();
builder.Services.AddSingleton<IReader, SqlReader>();
builder.Services.AddSingleton<IWriter, SqlWriter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();