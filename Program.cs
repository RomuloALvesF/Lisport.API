using System.Text;
using Scalar.AspNetCore;
using Lisport.API.Application.Services;
using Lisport.API.Domain.Interfaces;
using Lisport.API.Infra.Data;
using Lisport.API.Infra.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
