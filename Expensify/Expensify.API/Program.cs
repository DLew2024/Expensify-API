using Expensify.API.Configurations;
using Expensify.API.Migrations.Extenstions;
using Expensify.API.ServiceClasses;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.Utility.Validators.Filters;
using Expensify.API.Utility.Validators.Filters.Auth;
using Expensify.Services.Interfaces;
using FluentValidation;
using Scalar.AspNetCore;
using static Expensify.API.Utility.Constants;

var builder = WebApplication.CreateBuilder(args);
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

builder.AddApplicationDatabaseDB();
builder.AddApplicationAuthentication();

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddValidation();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        CorsPolicies.ReactFrontend,
        policy =>
        {
            policy.WithOrigins(allowedOrigins!).AllowAnyHeader().AllowAnyMethod();
        }
    );
});

builder.Services.AddValidatorsFromAssemblyContaining<LoginUserDTOValidator>();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<FrontendSettings>(builder.Configuration.GetSection("FrontendSettings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

// Add Scope for Service Class
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IService, Service>();
builder.Services.AddScoped(typeof(ValidationFilter<>));

// Add Transient for Validator
//builder.Services.AddTransient<IValidator<DTO>, DTOValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors(CorsPolicies.ReactFrontend);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.MapHealthChecks("health-check");

app.MigrateDB();

app.Run();
