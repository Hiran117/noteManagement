using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<NoteContext>();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtSecret"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// MongoDB service
builder.Services.AddSingleton<IMongoClient, MongoClient>(s => 
    new MongoClient(builder.Configuration.GetSection("MongoDB:ConnectionString").Value));

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builderPolicy => 
        builderPolicy.WithOrigins("http://localhost:3000")
                     .AllowAnyMethod()
                     .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
