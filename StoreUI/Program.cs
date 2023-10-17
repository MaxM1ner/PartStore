using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StoreUI.Data;
using StoreUI.Models;
using StoreUI.Services;
using System.Net;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Maxim") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<Customer, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders().AddDefaultUI().AddDefaultTokenProviders();

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
    });
    opts.AddPolicy("User", policy =>
    {
        policy.RequireRole("User");
    });
    
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/Account/Login";
        options.AccessDeniedPath = "/";
    });

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IEmailSender, SmtpEmailSender>((x) =>
{
    return new SmtpEmailSender(
    (int)(builder.Configuration.GetSection("SmtpCredential")?.GetValue(typeof(int), "SmtpClientPort") ?? throw new NullReferenceException("Configuration section SmtpCredential, value SmtpClientPort returned null")),
    (string)(builder.Configuration.GetSection("SmtpCredential")?.GetValue(typeof(string), "SmtpClientHost") ?? throw new NullReferenceException("Configuration section SmtpCredential, value SmtpClientHost returned null")),
    (string)(builder.Configuration.GetSection("SmtpCredential")?.GetValue(typeof(string), "Email") ?? throw new NullReferenceException("Configuration section SmtpCredential, value Email returned null")),
    (string)(builder.Configuration.GetSection("SmtpCredential")?.GetValue(typeof(string), "Password") ?? throw new NullReferenceException("Configuration section SmtpCredential, value Password returned null")),
    $"noreply@{builder.Environment.ApplicationName}.com");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
  name: "Admin",
  pattern: "{area:exists}/{controller=Roles}/{action=Index}/{id?}"
);

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");




app.MapRazorPages();

app.Run();
