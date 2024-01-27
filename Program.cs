

using Online_Survey.Middlewares;
using Online_Survey.Models;
using Online_Survey.Services.Base;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<DatabaseConfigurations>(
    builder.Configuration.GetSection("DatabaseConfigurations"));


builder.Services.AddSingleton<BaseService<TestCollection>>();


// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
