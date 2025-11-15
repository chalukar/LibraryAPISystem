using Library.Domain.Repositories;
using Library.gRPC.Services;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("LibraryDb")));

// Add services to the container.
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBorrowerRepository, BorrowerRepository>();
builder.Services.AddScoped<ILendingRecordRepository, LendingRecordRepository>();

builder.Services.AddAutoMapper(typeof(Library.Application.Mapping.LibraryProfile));

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.Load("Library.Application")));

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.MapGrpcService<Library.gRPC.Services.LibraryGrpcService>();

app.MapGet("/", () => "gRPC endpoint");

app.Run();