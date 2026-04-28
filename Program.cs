using System.Text;
using Scalar.AspNetCore;
using Lisport.API.Application.Services;
using Lisport.API.Application.Settings;
using Lisport.API.Domain.Enums;
using Lisport.API.Domain.Interfaces;
using Lisport.API.Infra.Data;
using Lisport.API.Infra.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
=======
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
>>>>>>> df4a018882a686e4ea631a2b898d710c7d421f71
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
<<<<<<< HEAD
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IPresencaRepository, PresencaRepository>();
builder.Services.AddScoped<IPresencaService, PresencaService>();
builder.Services.AddScoped<IEvolucaoRepository, EvolucaoRepository>();
builder.Services.AddScoped<IEvolucaoService, EvolucaoService>();
builder.Services.AddScoped<IObservacaoGeralRepository, ObservacaoGeralRepository>();
builder.Services.AddScoped<IObservacaoGeralService, ObservacaoGeralService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IRelatorioPatrocinadorService, RelatorioPatrocinadorService>();
=======
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAttendanceSessionRepository, AttendanceSessionRepository>();
builder.Services.AddScoped<IAttendanceRecordRepository, AttendanceRecordRepository>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IEvolutionRepository, EvolutionRepository>();
builder.Services.AddScoped<IEvolutionService, EvolutionService>();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.Configure<AdminSeedSettings>(builder.Configuration.GetSection("AdminSeed"));

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
if (jwtSettings == null)
{
    throw new InvalidOperationException("JWT settings not configured.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ClockSkew = TimeSpan.Zero
        };
    });
>>>>>>> df4a018882a686e4ea631a2b898d710c7d421f71

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=lisport.db"));

var jwtKey = builder.Configuration["Jwt:SecretKey"] ?? "Lisport-MVP-SuperSecretKey-Min32Chars!";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "Lisport.API",
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "Lisport.API",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/swagger");
}

app.UseHttpsRedirection();
<<<<<<< HEAD
app.UseCors();
=======

app.UseCors("Frontend");

>>>>>>> df4a018882a686e4ea631a2b898d710c7d421f71
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seedOptions = scope.ServiceProvider.GetRequiredService<IOptions<AdminSeedSettings>>().Value;
    if (seedOptions.Enabled &&
        !string.IsNullOrWhiteSpace(seedOptions.Email) &&
        !string.IsNullOrWhiteSpace(seedOptions.Password) &&
        !string.IsNullOrWhiteSpace(seedOptions.Name))
    {
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
        var existing = userRepository.GetByEmail(seedOptions.Email);

        if (existing == null)
        {
            authService.Register(
                seedOptions.Name,
                seedOptions.Email,
                seedOptions.Password,
                UserRole.Gestor,
                createdByUserId: null,
                mustChangePassword: true);
        }
    }
}

app.Run();
