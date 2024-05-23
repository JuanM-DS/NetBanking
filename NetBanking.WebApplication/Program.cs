using NetBanking.Infrastructure.ExtensionMethods;
using NetBanking.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Configurations
var configurations = builder.Configuration;

builder.Services.AddMvc().AddNewtonsoftJson();

builder.Services.AddDbContext(configurations);

builder.Services.AddServices();

builder.Services.AddOptions(configurations);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers(option => option.Filters.Add<GlobalExceptions>());
#endregion

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

app.Run();
