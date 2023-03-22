using Application;
using Application.Common.Exporters;
using Application.Common.Persistence;
using Ardalis.Endpoints.Infrastructure.Persistence.Repository;
using FSH.WebApi.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Excel;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddSingleton(typeof(IExcelReader), typeof(ExcelReader));
builder.Services.AddSingleton(typeof(IExcelWriter), typeof(ExcelWriter));
builder.Services.AddScoped(typeof(IRepository<>), typeof(ApplicationDbRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ApplicationDbRepository<>));

// builder.Services.AddScoped(typeof(IReadRepository<>), typeof(CachedRepository<>));
// builder.Services.AddScoped(typeof(ApplicationDbRepository<>));

builder.Services.AddControllers();

builder.Services.AddApplication();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
object p = builder.Services.AddSwaggerGen();

var provider = builder.Services.BuildServiceProvider();
//var configuration = provider.GetService<IConfiguration>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
builder.Services.AddPersistence(builder.Configuration);


var sinkOptions = new MSSqlServerSinkOptions
{
    AutoCreateSqlTable = true,
    TableName = "Logs"
};

builder.Host.UseSerilog((ctx, lc) => lc
.WriteTo.Console()
.WriteTo.Seq("http://localhost:5341")
.WriteTo.MSSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    sinkOptions: sinkOptions));

#region API Versioning
// Add API Versioning to the Project
builder.Services.AddApiVersioning(config =>
{
    // Specify the default API Version as 1.0
    config.DefaultApiVersion = new ApiVersion(1, 0);
    // If the client hasn't specified the API version in the request, use the default API version number 
    config.AssumeDefaultVersionWhenUnspecified = true;
    // Advertise the API versions supported for the particular endpoint
    config.ReportApiVersions = true;
});
#endregion

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
