using ApiPeople.Context;
using ApiPeople.Middlewares;
using ApiPeople.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container


//mysql managment

//var connectionString = builder.Configuration.GetConnectionString("Connection");
//var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));
//builder.Services.AddDbContext<AppDbContext>(
//    dbContextOptions => dbContextOptions
//        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
//        .LogTo(Console.WriteLine, LogLevel.Information)
//        .EnableSensitiveDataLogging()
//        .EnableDetailedErrors()
//);


builder.Services.AddDbContext<AppDbContext>(options => {
    var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Singleton
//builder.Services.AddScoped<IHelloWorldService, HelloworldService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserService, UserService>();

//lambda with params
builder.Services.AddScoped<IHelloWorldService>(p => new HelloworldService());
//in memory
//builder.Services.AddSingleton<IHelloWorldService, HelloworldService>();

//jwt tokens
var tokenString = builder.Configuration.GetValue("string", "JwtSecret");
var key = Encoding.ASCII.GetBytes(tokenString);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseTimeMiddleware();

app.MapControllers();

//app.MapGet("/api/tasks", async ([FromServices] AppDbContext dbContext) =>
//{
//    return Results.Ok(dbContext.Task.Include(p => p.Category).Where(p => p.Priority == ApiPeople.Models.Priority.Low));
//});

//app.MapPost("/api/tasks", async ([FromServices] AppDbContext dbContext, [FromBody] ApiPeople.Models.Task task) =>
//{
//    task.TaskId = Guid.NewGuid();
//    task.created_at = DateTime.Now;
//    await dbContext.AddAsync(task);

//    await dbContext.SaveChangesAsync();

//    return Results.Ok();

//});

//app.MapPut("/api/tasks/{id}", async ([FromServices] AppDbContext dbContext, [FromBody] ApiPeople.Models.Task task, [FromRoute] Guid id) =>
//{
//    var currentTaskl = dbContext.Task.Find(id);

//    if (currentTaskl != null)
//    {
//        currentTaskl.CategoryId = task.CategoryId;
//        currentTaskl.Tittle = task.Tittle;
//        currentTaskl.Priority = task.Priority;
//        currentTaskl.Description = task.Description;

//        await dbContext.SaveChangesAsync();

//        return Results.Ok();

//    }

//    return Results.NotFound();
//});


//app.MapDelete("/api/tareas/{id}", async ([FromServices] AppDbContext dbContext, [FromRoute] Guid id) =>
//{
//    var currentTaskl = dbContext.Task.Find(id);

//    if (currentTaskl != null)
//    {
//        dbContext.Remove(currentTaskl);
//        await dbContext.SaveChangesAsync();

//        return Results.Ok();
//    }

//    return Results.NotFound();
//});


app.Run();
