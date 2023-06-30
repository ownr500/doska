using System.IdentityModel.Tokens.Jwt;
using System.Text;
using doska.Data;
using doska.Data.Entities;
using doska.Options;
using doska.Services;
using doska.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddIdentity<User, Role>(cfg =>
    {
        cfg.User.RequireUniqueEmail = true;
        cfg.Password.RequireDigit = false;
        cfg.Password.RequiredLength = 6;
        cfg.Password.RequiredUniqueChars = 0;
        cfg.Password.RequireLowercase = false;
        cfg.Password.RequireNonAlphanumeric = false;
        cfg.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddUserManager<UserManager<User>>()
    .AddRoleManager<RoleManager<Role>>();
// Add services to the container.
builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config.GetSection("JWT")["ValidIssuer"],
            ValidAudience = config.GetSection("JWT")["ValidAudience"],
            IssuerSigningKey = 
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT")["Secret"]))
        };
    });



builder.Services.AddValidatorsFromAssemblyContaining<CreatePostRequestValidator>(includeInternalTypes: true);
builder.Services.AddAuthorization();
builder.Services.AddControllers()
    .AddFluentValidation();
builder.Services.AddDbContext<AppDbContext>(options => options
        .UseSqlServer(connectionString)
    .UseLazyLoadingProxies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo
    {
        Version = "V1",
        Title = "WebAPI",
        Description = "doska Web API"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentification with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
    options.EnableAnnotations();
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ISignInService, SignInService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<JwtSecurityTokenHandler>();
builder.Services.AddScoped<IPermissionsService, PermissionsService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 750 * 1024;
    options.ValueCountLimit = 5;
});
builder.Services.Configure<JwtOptions>(config.GetSection("JWT"));
builder.Services.Configure<PostOptions>(config.GetSection(nameof(PostOptions)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/V1/swagger.json", "doska Web API"); });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
