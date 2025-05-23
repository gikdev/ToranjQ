using Scalar.AspNetCore;
using ToranjQ.Api.Mapping;
using ToranjQ.App;
using ToranjQ.App.Database;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddApp();
builder.Services.AddDb(config["Database:ConnectionString"]!);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("ToranjQ API")
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.RestSharp)
            .WithDownloadButton(true)
            .WithLayout(ScalarLayout.Classic)
            .WithTheme(ScalarTheme.DeepSpace)
            .WithClientButton(true)
            .WithDarkModeToggle(true);
    });
}

app.UseDefaultFiles();
app.MapStaticAssets();
app.MapFallbackToFile("/index.html");

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ValidationMappingMiddleware>();

app.MapControllers();

var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();
