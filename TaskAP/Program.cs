using Asp.Versioning;
using TaskAP.Middleware;
using TaskAP.Repo;
using TaskAP.Service;
using TaskAP.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IProductRepo, ProductRepo>();
builder.Services.AddSingleton<IProductService, ProductService>();

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);

        options.AssumeDefaultVersionWhenUnspecified = true;

        options.ReportApiVersions = true;

        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddMvc();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();