using ApiSindisure.Apps.Login;
using ApiSindisure.Apps.AccountsPayable;
using ApiSindisure.Apps.AccountsReceivable;
using ApiSindisure.Apps.Condominium;
using ApiSindisure.Apps.Audit;
using ApiSindisure.Domain.Interfaces.Apps.Login;
using ApiSindisure.Domain.Interfaces.Apps.AccountsPayable;
using ApiSindisure.Domain.Interfaces.Apps.AccountsReceivable;
using ApiSindisure.Domain.Interfaces.Apps.Condominium;
using ApiSindisure.Domain.Interfaces.Apps.Audit;
using ApiSindisure.Services.Supabase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiSindisure.Domain.Interfaces.Apps.Buildings;
using ApiSindisure.Apps.Buildings;
using ApiSindisure.Domain.Interfaces.Apps.Companies;
using ApiSindisure.Apps.Companies;
using ApiSindisure.Domain.Interfaces.Apps.CompaniesRecurring;
using ApiSindisure.Apps.CompaniesRecurring;
using ApiSindisure.Domain.Interfaces.Apps.UserPermissions;
using ApiSindisure.Apps.UserPermissions;
using ApiSindisure.Domain.Interfaces.Apps.UserPlans;
using ApiSindisure.Apps.UserPlans;
using ApiSindisure.Domain.Interfaces.Apps.EmailAutomation;
using ApiSindisure.Apps.EmailAutomation;
using ApiSindisure.Domain.Interfaces.Apps.EmailReportsHistory;
using ApiSindisure.Apps.EmailReportsHistory;
using ApiSindisure.Domain.Interfaces.Apps.EmailReportsApp;
using ApiSindisure.Apps.EmailReportsApp;
using Resend;
using ApiSindisure.Domain.Interfaces.Apps.NotificationHistory;
using ApiSindisure.Apps.NotificationHistory;
using ApiSindisure.Domain.Interfaces.Apps.MessageSupport;
using ApiSindisure.Apps.MessageSupport;
using ApiSindisure.Domain.Interfaces.Apps.UserDetails;
using ApiSindisure.Apps.UserDetails;
using ApiSindisure.Apps.UserPlansHistory;
using ApiSindisure.Domain.Interfaces.Apps.UserPlansHistory;
using ApiSindisure.Domain.Interfaces.Apps.UserProfiles;
using ApiSindisure.Apps.UserProfiles;
using ApiSindisure.Domain.Interfaces.Apps.MessageSupportHistory;
using ApiSindisure.Apps.MessageSupportHistory;
using ApiSindisure.Domain.Interfaces.Apps.FileUpload;
using ApiSindisure.Apps.FileUpload;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SupabaseService>();
builder.Services.AddSingleton<ILoginApp, LoginApp>();
builder.Services.AddSingleton<IAccountsPayableApp, AccountsPayableApp>();
builder.Services.AddSingleton<IAccountsReceivableApp, AccountsReceivableApp>();
builder.Services.AddSingleton<ICondominiumApp, CondominiumApp>();
builder.Services.AddSingleton<IBuildingsApp, BuildingsApp>();
builder.Services.AddSingleton<ICompaniesApp, CompaniesApp>();
builder.Services.AddSingleton<IAuditApp, AuditApp>();
builder.Services.AddSingleton<ICompaniesRecurringApp, CompaniesRecurringApp>();
builder.Services.AddSingleton<IUserPermissionsApp, UserPermissionsApp>();
builder.Services.AddSingleton<IUserPlansApp, UserPlansApp>();
builder.Services.AddSingleton<IUserPlansHistoryApp, UserPlansHistoryApp>();
builder.Services.AddSingleton<IUserDetailsApp, UserDetailsApp>();
builder.Services.AddSingleton<IUserProfilesApp, UserProfilesApp>();
builder.Services.AddSingleton<IEmailAutomationApp, EmailAutomationApp>();
builder.Services.AddSingleton<IEmailReportsHistoryApp, EmailReportsHistoryApp>();
builder.Services.AddSingleton<INotificationHistoryApp, NotificationHistoryApp>();
builder.Services.AddSingleton<IMessageSupportApp, MessageSupportApp>();
builder.Services.AddSingleton<IMessageSupportHistoryApp, MessageSupportHistoryApp>();
builder.Services.AddSingleton<IAuditApp, AuditApp>();
builder.Services.AddSingleton<IFileUploadApp, FileUploadApp>();

builder.Services.AddScoped<IEmailReportsApp, EmailReportsApp>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(
            "https://appsindisure.com.br",
            "http://appsindisure.com.br"
        )
        .AllowAnyHeader()
        .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSecret = builder.Configuration["Supabase:JwtSecret"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Supabase:Issuer"],
        ValidAudience = "authenticated",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
    };
});

builder.Services.Configure<ResendClientOptions>(options =>
{
    options.ApiToken = builder.Configuration["Resend:ApiKey"]; // pega do appsettings.json
});

builder.Services.AddHttpClient<ResendClient>();


var app = builder.Build();

app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
