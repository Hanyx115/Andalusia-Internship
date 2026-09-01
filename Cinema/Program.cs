using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Cinema.Api.Services;
using Cinema.Data;
using Cinema.Mapping;
using Cinema.Middleware;
using Cinema.Repisitories;
using Cinema.Repistories;
using Cinema.Repistories.Interfaces;
using Cinema.Services;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);


// add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        //enum to be string
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

builder.Services.AddApiVersioning(options =>
{
options.DefaultApiVersion = new ApiVersion(1, 0);
options.AssumeDefaultVersionWhenUnspecified = true;
options.ReportApiVersions = true;
}).AddMvc().AddApiExplorer(options =>
{
options.GroupNameFormat = "'v'VVV";
options.SubstituteApiVersionInUrl = true;
});

// swagger configuration
builder.Services.AddSwaggerGen(options =>
{
//swagger for each api doc
options.DocInclusionPredicate((docName, apiDesc) => docName == apiDesc.GroupName);
});
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

// repo data access only
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IAuditoriumRepository, AuditoriumRepository>();
builder.Services.AddScoped<IShowTimeRepository, ShowTimeRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

// Services business logic
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IAuditoriumService, AuditoriumService>();
builder.Services.AddScoped<IShowTimeService, ShowTimeService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IBookingService, BookingService>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();




app.UseHttpsRedirection();

app.UseAuthorization();

try
{
    app.MapControllers();
}
catch (ReflectionTypeLoadException ex)
{
    foreach (var error in ex.LoaderExceptions)
    {
        Console.WriteLine("================================");
        Console.WriteLine(error?.ToString());
    }

    throw;
}
app.Run();

// Implementing IConfigureOptions<SwaggerGenOptions> and registering it with
// ConfigureOptions<T>() (above) is the real Dependency Injection way to do
// this: IApiVersionDescriptionProvider is injected through the constructor,
// by the actual DI container, exactly like every repository and service in
// this project — no manually-built temporary container required.
internal sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Title = "Cinema Booking API",
                Version = description.ApiVersion.ToString()
            });
        }
    }
}