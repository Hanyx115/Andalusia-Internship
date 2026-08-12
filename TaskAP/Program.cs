//using TaskAP.Middleware;
using TaskAP.Repo;
using TaskAP.Service;
using TaskAP.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IProductRepo, ProductRepo>();
builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();

//app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
