using EconomicManagementAPP.Interfaces;
using EconomicManagementAPP.Models;
using EconomicManagementAPP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

var userAuthenticationPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

// Add services to the container.
builder.Services.AddControllersWithViews(options => 
{
    options.Filters.Add(new AuthorizeFilter(userAuthenticationPolicy));
});
builder.Services.AddTransient<IRepositorieAccounts, RepositorieAccounts>();
builder.Services.AddTransient<IRepositorieAccountTypes, RepositorieAccountTypes>();
builder.Services.AddTransient<IRepositorieCategories, RepositorieCategories>();
builder.Services.AddTransient<IRepositorieOperationTypes, RepositorieOperationTypes>();
builder.Services.AddTransient<IRepositorieTransactions, RepositorieTransactions>();
builder.Services.AddTransient<IRepositorieUsers, RepositorieUsers>();
builder.Services.AddTransient<IUserServices, UserService>();
builder.Services.AddTransient<IUserStore<Users>, UserStore>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<SignInManager<Users>>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddIdentityCore<Users>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme, options =>
{
    options.LoginPath = "/users/login";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
