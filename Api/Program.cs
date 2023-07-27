using System.Text;
using Api.Extensions;
using LoggerService;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


// Add services to the container.
builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddCors(options => options.AddPolicy("MyPolicy",
// builder =>
// {
//     builder.AllowAnyHeader()
//            .AllowAnyMethod()
//            .SetIsOriginAllowed((host) => true)
//            .AllowCredentials();
// }));


builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
//builder.Services.AddResult("http://185.165.118.211:4010/Message/GetDic");
//builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);


// for swagger authorization
builder.Services.AddSwaggerGen(c =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { jwtSecurityScheme, Array.Empty<string>() }
        });
}
);

// for JWT Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});

builder.Services.AddControllers();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();