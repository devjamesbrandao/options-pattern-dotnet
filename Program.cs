using Microsoft.Extensions.Options;
using MinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// 1st Option - no validation of null values
// reference: https://www.milanjovanovic.tech/blog/how-to-use-the-options-pattern-in-asp-net-core-7
builder.Services.Configure<Settings>(
    builder.Configuration.GetSection(nameof(Settings))  
);

// 2nd Option - with validation of null values
// reference: https://www.milanjovanovic.tech/blog/adding-validation-to-the-options-pattern-in-asp-net-core
builder.Services
       .AddOptions<Settings>()
       .BindConfiguration(nameof(Settings))
       .ValidateDataAnnotations()
       .ValidateOnStart();

var app = builder.Build();

app.MapGet("/options-pattern", (
    IOptions<Settings> options,
    IOptionsSnapshot<Settings> optionsSnapshot,
    IOptionsMonitor<Settings> optionsMonitor) => 
    {
        var response = new 
        {
            OptionsValue = options.Value.JWTKey, 
            OptionsSnapShotValue = optionsSnapshot.Value.JWTKey,
            MonitorValue = optionsMonitor.CurrentValue.JWTKey
        };

        return Results.Ok(response);
    }
);

app.Run();
