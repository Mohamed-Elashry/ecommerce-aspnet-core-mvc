using E_Commerce.Data;
using E_Commerce.Data.Cart;
using E_Commerce.Data.Localization;
using E_Commerce.Data.UOW;
using E_Commerce.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// DbContext Configurations
builder.Services
       .AddDbContext<AppDbContext>
       (options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
       );

#region Service Configuration   
builder.Services.AddScoped<IGeneralService,GeneralService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
#endregion

#region Localization
builder.Services.AddLocalization();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
builder.Services.AddMvc()
       .AddMvcLocalization(LanguageViewLocationExpanderFormat.Suffix)
       .AddDataAnnotationsLocalization(options =>
       {
           options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(JsonStringLocalizerFactory));
       });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCulture = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG"),
        new CultureInfo("ar-SA"),
        new CultureInfo("de-DE"),
    };
    options.DefaultRequestCulture = new RequestCulture(culture: supportedCulture[0],uiCulture: supportedCulture[0]);
    options.SupportedCultures = supportedCulture;
    options.SupportedUICultures = supportedCulture;
});


#endregion
//Authentication and Authorization
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});
// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseSession();
//Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

#region Complete Localization Configurations
var supportedCultures = new[] { "en-US", "ar-EG", "ar-SA", "de-DE" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);    
#endregion

app.UseAuthorization();
// To use browser language
app.UseRequestCulture();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AppDbInitializer.SeedData(app).Wait();
app.Run();
