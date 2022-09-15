using IdentityApi.Data;
using IdentityApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IdentityApiContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection") ?? throw new InvalidOperationException("Connection string 'PostgresConnection' not found.")));

builder.Services.AddIdentityCore<IdentityUser>(opt =>
{
    opt.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityApiContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(opt =>
   {
       opt.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
               .GetBytes(builder.Configuration["JWTSettings:TokenKey"]))
       };
   });

builder.Services.AddAuthorization();

builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
