using core.contexts;
using core.services;

using entities.interfaces;
using entities.models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(option =>
    {
        option.JsonSerializerOptions.MaxDepth = 0;
        option.JsonSerializerOptions.WriteIndented = true;
        option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        option.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
    });

var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .Build();

builder
    .Services
    .Configure<entities.models.AppSetting>(appSetting =>
    {
        appSetting.googleMap = config["AppSettings:googleMap"];
        appSetting.sistemaURL = config["AppSettings:sistemaURL"];
        appSetting.reporteURL = config["AppSettings:reporteURL"];
        appSetting.sendgridKey = config["AppSettings:sendGridKey"];
        appSetting.directorioFisico = config["AppSettings:directorioFisico"];
        appSetting.directorioVirtual = config["AppSettings:directorioVirtual"];
        appSetting.key = config["Jwt:Key"];
        appSetting.issuer = config["Jwt:Issuer"];
        appSetting.audience = config["Jwt:Audience"];
        appSetting.tokenLifetime = System.TimeSpan.Parse(config["Jwt:TokenLifetime"]);
    })
    .Configure<MvcOptions>(options =>
    {
        options.Filters.Add(new RequireHttpsAttribute());
    })
    .AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseMySql(config.GetConnectionString("cnnConexion"), ServerVersion.AutoDetect(config.GetConnectionString("cnnConexion")),
            mySqlOptionsAction: mysqlOptions =>
            {
                mysqlOptions.EnableRetryOnFailure();
            }).EnableSensitiveDataLogging(true);
    })
    .AddCors(corsOption => corsOption.AddPolicy("ReportingRestPolicy", corsBuilder =>
    {
        corsBuilder
        .WithOrigins("https://localhost:44398/")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    }));

TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = config["Jwt:Issuer"],
    ValidAudience = config["Jwt:Audience"],
    RequireExpirationTime = false,
    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["Jwt:Key"])),
    ClockSkew = System.TimeSpan.Zero
};

builder.Services.Configure<ServiceConfiguration>(config.GetSection("ServiceConfiguration"));

builder
    .Services
    .AddSingleton(typeof(IHttpContextAccessor), typeof(HttpContextAccessor))
    .AddSingleton(tokenValidationParameters);

#region repositories

builder.Services.AddInfrastructure();

#endregion repositories

#region services

builder
    .Services
    .AddTransient<IDbConnectionFactory>(provider => provider.GetService<MySqlConnectionFactory>())
    .AddTransient<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>())
    .AddTransient<IIdentityService, IdentityService>()
    .AddTransient<IUserService, UserService>();

#endregion services

builder
    .Services
    .AddAuthorization(options =>
    {
        options.AddPolicy("bearer", new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());
    })
    .AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Audience = config.GetSection("Jwt:Audience").Value;
        options.ClaimsIssuer = config.GetSection("Jwt:Issuer").Value;
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = tokenValidationParameters;
        options.Events = new JwtBearerEvents()
        {
            OnTokenValidated = context =>
            {
                var accessToken = context.SecurityToken as JwtSecurityToken;
                if (accessToken != null)
                {
                    ClaimsIdentity identity = context.Principal.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        identity.AddClaim(new Claim("access_token", accessToken.RawData));
                    }
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var culture = CultureInfo.CreateSpecificCulture("es-MX");
var dateformat = new DateTimeFormatInfo
{
    ShortDatePattern = "dd/MM/yyyy",
    LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
};
culture.DateTimeFormat = dateformat;

var supportedCultures = new[] { culture };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culture),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();