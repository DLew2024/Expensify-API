using Expensify.API.Authentication.Handlers;
using Expensify.API.Configurations;
using Expensify.API.Development;
using Expensify.API.Migrations.Extenstions;
using Expensify.API.ServiceClasses;
using Expensify.API.ServiceClasses.Interfaces;
using Expensify.API.ServiceClasses.Interfaces.Resolvers;
using Expensify.API.ServiceClasses.Resolvers;
using Expensify.API.Utility.Validators.Filters;
using Expensify.API.Utility.Validators.Filters.Auth;
using Expensify.API.Utility.Validators.Filters.Income;
using Expensify.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using static Expensify.API.Utility.Constants;

var builder = WebApplication.CreateBuilder(args);
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

if (builder.Environment.IsDevelopment())
{
    builder
        .Services.AddAuthentication("Development")
        .AddScheme<AuthenticationSchemeOptions, DevelopmentAuthenticationHandler>(
            "Development",
            _ => { }
        );
}
else
{
    builder.AddApplicationAuthentication();
}

builder.AddApplicationDatabaseDB();

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer(
        (document, context, cancellationToken) =>
        {
            document.Components ??= new OpenApiComponents();

            document.Components.SecuritySchemes ??=
                new Dictionary<string, IOpenApiSecurityScheme>();

            document.Components.SecuritySchemes[JwtBearerDefaults.AuthenticationScheme] =
                new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant(),
                    BearerFormat = "JWT",
                    Description = "Enter your JWT Bearer token.",
                };

            return Task.CompletedTask;
        }
    );
});
builder.Services.AddValidation();

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
builder.Services.AddValidatorsFromAssemblyContaining<AddIncomeTransactionDTOValidator>();

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
builder.Services.AddScoped<IAccountResolver, AccountResolver>();
builder.Services.AddScoped<ITransactionResolver, TransactionResolver>();
builder.Services.AddScoped<IService, Service>();
builder.Services.AddScoped(typeof(ValidationFilter<>));

var app = builder.Build();

await app.MigrateDBAsync();

if (app.Environment.IsDevelopment())
{
    await app.SeedDevelopmentDataAsync();

    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/openapi/{documentName}.json");
    });
}

app.UseHttpsRedirection();

app.UseCors(CorsPolicies.ReactFrontend);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.MapHealthChecks("health-check");

app.Run();
