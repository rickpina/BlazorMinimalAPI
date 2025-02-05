using BlazorMinimalAPI.Components;
using BlazorMinimalAPI.Data_Models;
using BlazorMinimalAPI.Database_Logic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

Console.WriteLine("Web Api's Base URL");
Console.WriteLine(builder.Configuration["API_BASE_URL"]);
Console.WriteLine("Container Database Connection String");
Console.WriteLine(builder.Configuration["RawConnection"]);

builder.Configuration.AddUserSecrets<Program>();

var apiBaseUrl = builder.Configuration["API_BASE_URL"] ?? "https://localhost:7181/";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });


try
{
    string connectionstring = builder.Configuration["RawConnection"] 
        ?? builder.Configuration.GetConnectionString("LocalConnection") 
        ?? throw new Exception("Connection String is null!");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(connectionstring));
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


var weatherForcastBaseURL = app.MapGroup("/weatherforecast");
var personBaseURL = app.MapGroup("/person");


#region Person API

personBaseURL.MapPost("/generate", async (ApplicationDbContext dbContext, GeneratePerson person, HttpContext httpContext) =>
{
    var random = new Random();
    List<Person> personList = new List<Person>();

    for (int i = 0; i < person.GenerateAmount; i++)
    {
        var generatedPerson = new Person
        {
            FirstName = person.FirstName == "Random"
                ? RandomData.FirstNames[random.Next(RandomData.FirstNames.Count)]
                : person.FirstName,
            LastName = person.LastName == "Random"
                ? RandomData.LastNames[random.Next(RandomData.LastNames.Count)]
                : person.LastName,
            Occupation = person.Occupation == "Random"
                ? RandomData.Occupations[random.Next(RandomData.Occupations.Count)]
                : person.Occupation,
            DateOfBirth = RandomData.GenerateRandomDateOfBirth(),
            SSN = RandomData.GenerateFakeSSN(),
            Title = person.Title == "Random"
                ? RandomData.Titles[random.Next(RandomData.Titles.Count)]
                : person.Title,
        };

        personList.Add(generatedPerson);
    }

    await dbContext.People.AddRangeAsync(personList);
    await dbContext.SaveChangesAsync();

    return Results.Ok(personList);
});


personBaseURL.MapGet("", async (ApplicationDbContext db) =>
    await db.People.ToListAsync());

#endregion 


#region Weather API
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

weatherForcastBaseURL.MapGet("/", (HttpContext httpContext) =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        })
        .ToArray();
    return forecast;
});

#endregion 

app.Run();
