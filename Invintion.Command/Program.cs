using Calzolari.Grpc.AspNetCore.Validation;
using Invitation.Command.Services;
using Invitation.Command.Infrastructure.database;
using Microsoft.EntityFrameworkCore;
using Invitation.Command.Abstractions.Persistence;
using Invitation.Command.Infrastructure;
using Serilog;
using Invitation.Command.Interceptors;
using Invitation.Command.Logging;
using Invitation.Command.Extensions.Services;

Log.Logger = LoggerServiceBuilder.Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddGrpc(option => {
//    option.Interceptors.Add<HandleErrorInterceptor>();
//});
builder.Services.AddGrpcWithValidators();
builder.Services.AddMediatR(o => o.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddDbContext<InvitationDbContext>(
   o => o.UseSqlServer("")
    );
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<InvitationService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

public partial class Program { }