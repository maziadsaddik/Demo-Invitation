using Calzolari.Grpc.AspNetCore.Validation;
using Invitation.Command.Services;
using Invitation.Command.Infrastructure.database;
using Microsoft.EntityFrameworkCore;
using Invitation.Command.Abstractions.Persistence;
using Invitation.Command.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(o => o.EnableMessageValidation());
builder.Services.AddGrpcValidation();

builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddDbContext<InvitationDbContext>(
   o => o.UseSqlServer("Server=localhost,4340;Database=Invitation;User Id=SA;Password=Wesam@204;Encrypt=True;TrustServerCertificate=true;")
    );
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Host.UseSerilog();

var app = builder.Build();

var scope = app.Services.CreateScope();
var database = scope.ServiceProvider.GetRequiredService<InvitationDbContext>();

database.Database.EnsureCreated();

// Configure the HTTP request pipeline.
app.MapGrpcService<InvitationService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

public partial class Program { }