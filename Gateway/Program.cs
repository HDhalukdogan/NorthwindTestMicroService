using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((context, cfg) =>
{
    cfg
    .SetBasePath(context.HostingEnvironment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true)
    .AddJsonFile("ocelot.json")
    .AddEnvironmentVariables();
});

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

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddOcelot();

var app = builder.Build();
//var configuration = new OcelotPipelineConfiguration
//{
//    AuthorizationMiddleware = async (ctx, next) =>
//    {
//        if (Authorize(ctx))
//        {
//            await next.Invoke();

//        }
//        else
//        {
//            //ctx.Errors.Add(new UnauthorisedError($"Fail to authorize"));
//        }
//    }
//};

//app.UseOcelot(configuration);
await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();

//static bool Authorize(HttpContext ctx)
//{
//    DownstreamRoute route = (DownstreamRoute)ctx.Items["DownstreamRoute"];
//    string key = route.AuthenticationOptions.AuthenticationProviderKey;

//    if (key == null || key == "") return true;
//    if (route.RouteClaimsRequirement.Count == 0) return true;
//    else
//    {
//        //flag for authorization
//        bool auth = false;

//        //where are stored the claims of the jwt token
//        Claim[] claims = ctx.User.Claims.ToArray<Claim>();

//        //where are stored the required claims for the route
//        Dictionary<string, string> required = route.RouteClaimsRequirement;

//        return auth;
//    }
//}
