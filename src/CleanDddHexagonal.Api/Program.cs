using CleanDddHexagonal.Api.Middleware;
using CleanDddHexagonal.Application.UseCases.Customers;
using CleanDddHexagonal.Application.UseCases.Reconciliation;
using CleanDddHexagonal.Application.UseCases.UsageRecords;
using CleanDddHexagonal.Infrastructure;
using CleanDddHexagonal.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CreateCustomerUseCase>();
builder.Services.AddScoped<GetCustomersUseCase>();
builder.Services.AddScoped<RegisterInternalUsageUseCase>();
builder.Services.AddScoped<ImportExternalUsageSnapshotUseCase>();
builder.Services.AddScoped<RunReconciliationUseCase>();
builder.Services.AddScoped<GetOpenIssuesUseCase>();
builder.Services.AddScoped<ResolveIssueUseCase>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=cloud-reconciliation-engine.db";

builder.Services.AddInfrastructure(connectionString);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
