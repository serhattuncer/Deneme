using Dastone.Extension;
using Dastone.Helpers;
using Dastone.HttpRequest;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.InitializeClientBaseAdress(builder.Configuration);
builder.Services.AddCustomHttpClients(builder.Configuration);
builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddCustomAuthorization();
builder.Services.AddCustomSession();
builder.Services.AddTransient(typeof(GenericRequestsClient<>));
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
   pattern: "{controller=Authentication}/{action=Login}/{id?}"
 );


app.Run();
