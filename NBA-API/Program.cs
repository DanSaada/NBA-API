using NBA_API.Models;
using NBA_API.Services;
using Newtonsoft.Json;

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Load GameData from JSON
var gamesData = LoadGamesDataFromJson("https://cdn.nba.com/static/json/liveData/playbyplay/playbyplay_0022000180.json");

// Register the game data and NBAService with the DI container
builder.Services.AddSingleton(gamesData);
builder.Services.AddSingleton<NBAService>();

// Add controllers
builder.Services.AddControllers()
        .AddNewtonsoftJson(); // Add NewtonsoftJson support

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NBA API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // Enable middleware to serve generated Swagger as a JSON endpoint
    app.UseSwagger();
    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NBA API v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();

GamesData LoadGamesDataFromJson(string jsonUrl)
{
    using (var client = new HttpClient())
    {
        var json = client.GetStringAsync(jsonUrl).Result;
        var gamesData = JsonConvert.DeserializeObject<GamesData>(json);
        return gamesData;
    }
}


