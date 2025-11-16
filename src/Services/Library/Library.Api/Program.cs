
using Library.Domain.Repositories;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Detect environment for tests
var isTesting = builder.Environment.IsEnvironment("Testing");

var configuration = builder.Configuration;

// DbContext (only SQL Server in non-testing env)
if (!isTesting)
{
    builder.Services.AddDbContext<LibraryDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("LibraryDb")));
}
else
{
    // For WebApplicationFactory override (no provider here)
    builder.Services.AddDbContext<LibraryDbContext>(options =>
        options.UseInMemoryDatabase("FunctionalTestDb"));
}


// Repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBorrowerRepository, BorrowerRepository>();
builder.Services.AddScoped<ILendingRecordRepository, LendingRecordRepository>();

// MediatR + CQRS
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.Load("Library.Application")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(Library.Application.Mapping.LibraryProfile));


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddOpenApi();

var app = builder.Build();

if (!isTesting)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    await db.Database.MigrateAsync();
    await SeedData.InitializeAsync(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Required for tests
public partial class Program { }
