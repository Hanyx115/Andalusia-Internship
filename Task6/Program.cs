using Microsoft.EntityFrameworkCore;
using Task6.Api.Services;
using Task6.Data;
using Task6.Repistories;
using Task6.Repistories.Interfaces;
using Task6.Services.Interfaces;
using TaskManager.Api.Repositories;
using TaskManager.Api.Repositories.Interfaces;
using TaskManager.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Repositories
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

// Services
builder.Services.AddScoped<ITasksService, TasksService>();
builder.Services.AddScoped<IUsersService, UsersService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();