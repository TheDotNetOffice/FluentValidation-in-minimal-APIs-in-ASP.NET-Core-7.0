using FluentValidation;
using FluentValidationInAspCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<Student>, StudentValidator>();
builder.Services.AddScoped<IstudentBuilder, StudentBuilder>();

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

app.MapPost("/student", async (IValidator<Student> validator, IstudentBuilder builder, Student student) =>
{
    var validationResult = await validator.ValidateAsync(student);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }
    builder.AddStudent(student);
    return Results.Created($"/{student.Id}", student);
});

app.Run();
