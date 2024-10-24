var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Replace AddMvcCore with AddControllers
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer(); // Required for minimal APIs
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Enable Swagger middleware conditionally
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Optional: Serve Swagger UI in production
    // app.UseSwagger();
    // app.UseSwaggerUI();

    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
