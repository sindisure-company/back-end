using ApiSindisure.Application.Jwt;
using ApiSindisure.Apps.Login;
using ApiSindisure.Apps.AccountsPayable;
using ApiSindisure.Apps.Audit;
using ApiSindisure.Domain.Interfaces.Apps.Login;
using ApiSindisure.Domain.Interfaces.Apps.AccountsPayable;
using ApiSindisure.Domain.Interfaces.Apps.Audit;
using ApiSindisure.Domain.Interfaces.Services.Jwt;
using ApiSindisure.Services.Supabase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<SupabaseService>();
builder.Services.AddSingleton<IJwtServices, JwtServices>();
builder.Services.AddSingleton<ILoginApp, LoginApp>();
builder.Services.AddSingleton<IAccountsPayableApp, AccountsPayableApp>();
builder.Services.AddSingleton<IAuditApp, AuditApp>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader(); 
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


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
