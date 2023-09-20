using PersonApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<List<Person>>(provider =>
{
    var persons = new List<Person>
        {
            new Person { Id = Guid.Parse("a7f394e6-8b23-4dce-9f61-9a1f8c2d7b89"), Name = "John", Age = 30 },
            new Person { Id = Guid.Parse("f2c1d8a9-5e56-4a77-bc84-3d0a6e12f4c7"), Name = "Alice", Age = 25 },
        };
    return persons;
});


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
