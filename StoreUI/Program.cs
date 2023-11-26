using DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Services;
using Entities.Models;
using StoreUI.Services;
using System.Net;
using System.Security.Claims;
using System.Text;
using ServiceContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found."); //DefaultConnection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<Customer, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders().AddDefaultUI();

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
        options.AccessDeniedPath = "/Home";
        options.Cookie.Expiration = TimeSpan.FromDays(1);
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
StringBuilder sb = new StringBuilder();
sb.Append(Environment.CurrentDirectory)
    .Append(Path.DirectorySeparatorChar).
    Append("wwwroot").
    Append(Path.DirectorySeparatorChar).
    Append("productFiles").
    Append(Path.DirectorySeparatorChar).
    Append("img").
    Append(Path.DirectorySeparatorChar);
builder.Services.AddTransient<FormImageManager>((x) =>
{
    return new FormImageManager(sb.ToString());
});
builder.Services.AddTransient<FeatureManager>();
builder.Services.AddTransient<ProductTypeManager>();
builder.Services.AddTransient<ProductManager>();
builder.Services.AddTransient<ProductCommentManager>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IOrdersService, OrdersService>();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
  name: "Admin",
  pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
);

app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");




app.MapRazorPages();

app.Run();
