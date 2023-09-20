using MedicalApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MedicalRecord>();
builder.Services.AddSingleton<List<MedicalRecord>>(provider =>
{
    var medicalRecords = new List<MedicalRecord>
{
    new MedicalRecord
    {
        Id = Guid.NewGuid(),PatientName = "John",Diagnosis = "Diagnosis 2",Treatment = "Treatment 2", PersonId = Guid.Parse("a7f394e6-8b23-4dce-9f61-9a1f8c2d7b89")
    },
    new MedicalRecord
    {
        Id = Guid.NewGuid(),PatientName = "Alice",Diagnosis = "Diagnosis 3",Treatment = "Treatment 3", PersonId = Guid.Parse("f2c1d8a9-5e56-4a77-bc84-3d0a6e12f4c7")
    }
};

    return medicalRecords;
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
