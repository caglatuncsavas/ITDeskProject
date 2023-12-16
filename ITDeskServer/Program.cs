using ITDeskServer.Context;
using ITDeskServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ITDeskServer.Middleware;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using ITDeskServer.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(configure =>
{
    configure.AddDefaultPolicy(policy =>
    {
        policy
        .AllowAnyHeader() //contentType => application/json application/est text/xml text/html mime type denir.
        .AllowAnyOrigin() //www.google.com www.facebook.com gibi sitelerden gelen isteklere izin veriyor.
        .AllowAnyMethod();//Get Post Put Delete gibi isteklere izin veriyor.
    });
});

#region Dependency Injection
builder.Services.TryAddScoped<JwtService>();
#endregion

#region Authentication
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true, //Tokeni gönderen kiþi bilgisi
        ValidateAudience = true, //Tokeni kullanacak site ya da kiþi bilgisi
        ValidateIssuerSigningKey = true, //Tokenin doðruluðunu doðrulayacak anahtar
        ValidateLifetime = true, // Tokenin yaþam süresini kontrol etmek istiyor musunuz?
        ValidIssuer = "Cagla Tunc Savas", //Tokeni gönderen kiþi bilgisi
        ValidAudience = "IT Desk Angular App", //Tokeni kullanacak site ya da kiþi bilgisi
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my secret key my secret key my secret key 1234...my secret key my secret key my secret key 1234..."))//Tokenin doðruluðunu doðrulayacak anahtar
    };
});
#endregion

#region DbContext ve Identity
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("Data Source=CAGLA\\SQLEXPRESS;Initial Catalog=ITDeskDbWithWebApi;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
});

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.SignIn.RequireConfirmedEmail = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    opt.Lockout.MaxFailedAccessAttempts = 2;
    opt.Lockout.AllowedForNewUsers = true;

})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region Presentation
builder.Services.AddControllers().AddOData(options=>options.EnableQueryFeatures());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});

#endregion


#region Middlewares
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ExtensionsMiddleware.AutoMigration(app);

ExtensionsMiddleware.CreateFirstUser(app);

app.Run();
#endregion

