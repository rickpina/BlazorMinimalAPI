using BlazorMinimalAPI.Components;
using BlazorMinimalAPI.Data_Models;
using BlazorMinimalAPI.Database_Logic;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7181/") } );

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




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

        dbContext.People.Add(generatedPerson);
        await dbContext.SaveChangesAsync();
        personList.Add(generatedPerson);
        
    }
    

    return Results.Ok(personList);
});



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
