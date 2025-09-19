using FacilAssistApi.Ports;
using FacilAssistApi.Repositories;
using FacilAssistApi.Services;
using FacilAssistApi.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddValidatorsFromAssemblyContaining<ClienteCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();

builder.Services.AddSwaggerGen(options =>
{    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IAuthRepository, AuthRepository>();




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {        
        options.TokenValidationParameters = new TokenValidationParameters
        {            
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],

            
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

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
