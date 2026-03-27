var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// bookmark saver
builder.Services.AddMemoryCache();

//connect to react
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // react address
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowReactApp");
app.MapControllers();

app.Run();