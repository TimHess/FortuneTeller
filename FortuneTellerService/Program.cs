using FortuneTellerService.Models;
using Steeltoe.Bootstrap.AutoConfiguration;
using Steeltoe.Configuration.CloudFoundry;
using Steeltoe.Configuration.CloudFoundry.ServiceBinding;
using Steeltoe.Configuration.ConfigServer;
using Steeltoe.Connectors.EntityFrameworkCore.PostgreSql;
using Steeltoe.Connectors.PostgreSql;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Management.Endpoint;

var builder = WebApplication.CreateBuilder(args);

// Automatically add referenced Steeltoe components
//builder.AddSteeltoe();

// Add Steeltoe components individually
builder.Configuration
    .AddCloudFoundry()
    .AddConfigServer(LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Trace).AddConsole()))
    .AddCloudFoundryServiceBindings();
builder.AddServiceDiscovery(options => options.UseEureka());
builder.AddAllActuators();
builder.AddPostgreSql();

builder.Services.AddDbContext<FortuneContext>((serviceProvider, options) => options.UseNpgsql(serviceProvider));
builder.Services.AddScoped<IFortuneRepository, FortuneRepository>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await PostgreSqlSeeder.CreateSampleDataAsync(app.Services);

app.Run();
