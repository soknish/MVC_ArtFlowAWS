using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC_ArtFlowAWS.Data;
using MVC_ArtFlowAWS.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MVC_ArtFlowAWSContextConnection") ?? throw new InvalidOperationException("Connection string 'MVC_ArtFlowAWSContextConnection' not found.");;

builder.Services.AddDbContext<MVC_ArtFlowAWSContext>(options => options.UseSqlServer(connectionString));
// important here dummies!! after setting up the email confirmation, change 'RequireConfirmedAccount = false' to true
builder.Services.AddDefaultIdentity<MVC_ArtFlowAWSUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<MVC_ArtFlowAWSContext>();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

// this is where the default route is set up (homepage)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapRazorPages();    


app.Run();
