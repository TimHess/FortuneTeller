using FortuneTellerUI.Services;
using Steeltoe.Bootstrap.AutoConfiguration;
using Steeltoe.Common.Http.LoadBalancer;
using Steeltoe.Configuration.CloudFoundry;
using Steeltoe.Configuration.CloudFoundry.ServiceBinding;
using Steeltoe.Configuration.ConfigServer;
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


builder.Services.Configure<FortuneServiceOptions>(builder.Configuration.GetSection("fortuneService"));
builder.Services.AddScoped<IFortuneService, FortuneServiceClient>();
builder.Services.AddHttpClient<IFortuneService, FortuneServiceClient>().AddRoundRobinLoadBalancer();


builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
