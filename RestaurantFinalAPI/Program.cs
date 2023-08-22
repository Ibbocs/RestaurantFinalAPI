using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using RestaurantFinalAPI.Application.Validators.UserValid;
using RestaurantFinalAPI.Infrastructure.Registrations;
using RestaurantFinalAPI.Persistance.Context;
using RestaurantFinalAPI.Persistance.Registration;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Collections.ObjectModel;
using System.Data;
using Serilog.Core;
using RestaurantFinalAPI.Extensions;
using RestaurantFinalAPI.Middlewares;
using Serilog.Context;
using RestaurantFinalAPI.Registrations;
using RestaurantFinalAPI.Application.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
                                  //AutoRegiter edecek validator claslari, hannsi ki createuservaldator olan assemblydedi.
builder.Services.AddControllers().AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CreatUserValidator>());

//All Percistance Layer's Service Registrartion
builder.Services.AddPersistanceServices();
//builder.Services.AddDbContext<RestaurantFinalAPIContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantApiDB")));

//All Infrastructure Layer's Service Registration
builder.Services.AddInfrastructureService();

//All Presentation Layer's Service Registration
builder.Services.AddPresentationService();

//Aplication
builder.Services.AddAutoMapper(typeof(MappingProfile));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//serilog
//Serilog.Sinks.MSSqlServer 5.8.0 calisdi
//6.0.0> ucun https://stackoverflow.com/questions/73013096/net-6-serilog-is-not-creating-log-table
Logger? log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/mylog-{Date}.txt")
    .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("RestaurantApiDB"), sinkOptions: new MSSqlServerSinkOptions
    {
        TableName = "RALog",
        AutoCreateSqlTable = true
    },
    null, null, LogEventLevel.Warning, null,
    columnOptions: new ColumnOptions
    {
        AdditionalColumns = new Collection<SqlColumn>
        {
                new SqlColumn(columnName:"User_Name", SqlDbType.NVarChar)
        }
    },
     null, null
     )
    .WriteTo.Seq(builder.Configuration["Seq:ServerUrl"])
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//global exception
//todo bu niye islemir ay qardas
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

//serilogun requestleri loglamasi ucun ne qeder evvelde olsa yaxsidi
app.UseSerilogRequestLogging();
app.UseHttpLogging();

//Using Cros policy
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//logda db'ya user_name yazdirmaq ucun
app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("User_Name", username);
    await next();
});

app.MapControllers();

app.Run();
