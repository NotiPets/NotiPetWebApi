using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Notipet.Data;
using Utilities;

var builder = WebApplication.CreateBuilder(args);

//jwt 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = String.Empty;

// Check if exists the local secret (so probably dealing with dev)
if (builder.Configuration["DATABASE_CONNECTION_STRING"] != null)
{
    connectionString = builder.Configuration["DATABASE_CONNECTION_STRING"];
}
// else, go get it from an environment variable (so probably production)
else
{
    connectionString = Methods.GetConnectionString();
}

using (var context = new NotiPetBdContext(new DbContextOptionsBuilder<NotiPetBdContext>().UseNpgsql(connectionString).Options))
{
    await context.Database.MigrateAsync();
    Console.WriteLine("Database up to date");
}

builder.Services.AddDbContext<NotiPetBdContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

app.MapGet("/", () => "All working!");

app.UseCors(x => x.AllowAnyHeader()
                  .AllowAnyMethod()
                  //.AllowAnyOrigin()
                  .SetIsOriginAllowed(origin => true)
                  .AllowCredentials());

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
