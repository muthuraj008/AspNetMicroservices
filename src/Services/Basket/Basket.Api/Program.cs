using Basket.Api.Repository;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();
// Add services to the container.
//added redis 
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = config.GetValue<string>("CacheSettings:ConnectionString");
    options.InstanceName = "master";
    }
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//controller
builder.Services.AddScoped<IBasketRepository, BasketRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
