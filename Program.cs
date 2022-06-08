using BudgetMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BudgetMVC.Data;
using BudgetMVC.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BudgetMVCContextConnection") ?? throw new InvalidOperationException("Connection string 'BudgetMVCContextConnection' not found.");

builder.Services.AddDbContext<BudgetMVCContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<BudgetMVCUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BudgetMVCContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TransactionContext>(
    opt => opt.UseSqlServer(@"Server=localhost;Database=BudgetDB;Trusted_Connection=True;")
    );
builder.Services.AddTransient<TransactionContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Transactions}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
