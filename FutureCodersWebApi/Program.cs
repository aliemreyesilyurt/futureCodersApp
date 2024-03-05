using FutureCodersWebApi.Extensions;
using NLog;
using Services.Contract;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
    .AddNewtonsoftJson();
//AddApplicationPart(), ozelligi ile birlikte controller yapisinin diger projeler dahilinde kullanilabilmesine olanak saglanir.)

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// IoC Semasindaki Register blogu
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();


var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
