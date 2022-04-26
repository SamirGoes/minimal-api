using Bogus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<Faker>(provider => new Faker("pt_BR"));

var app = builder.Build();

app.MapGet("/customer", (Faker faker) => {
    int ids = 1;
    return Enumerable.Range(1, 10)
        .Select(c => new Customer(ids++, faker.Name.FullName()));
});

app.MapGet("/customer/{id:int}", async(Faker faker, int id) =>
    await Task.FromResult(new Customer(id, faker.Name.FullName()))
);

app.Run();

internal record Customer(int Id, string Name);