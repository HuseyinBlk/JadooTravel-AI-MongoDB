using System.Reflection;
using JadooTravel.Entities;
using JadooTravel.Services.BookingServices;
using JadooTravel.Services.CategoryServices;
using JadooTravel.Services.DestinationServices;
using JadooTravel.Services.FeatureServices;
using JadooTravel.Services.PartnerServices;
using JadooTravel.Services.SubscribeServices;
using JadooTravel.Services.TestimonialServices;
using JadooTravel.Services.TripPlanServices;
using JadooTravel.Services.LanguageServices;
using JadooTravel.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettingsKey"));
builder.Services.AddSingleton<IDatabaseSettings>(sp => { return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value; }
);

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IDatabaseSettings>();
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IDatabaseSettings>();
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDestinationService, DestinationService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<ITestimonialService, TestimonialService>();
builder.Services.AddScoped<ITripPlanService, TripPlanService>();
builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<ISubscribeService, SubscribeService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ILanguageService, LanguageService>();

var connectionString = builder.Configuration.GetValue<string>("DatabaseSettingsKey:ConnectionString");
var databaseName = builder.Configuration.GetValue<string>("DatabaseSettingsKey:DatabaseName");

builder.Services.AddIdentity<AppUser, AppRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 3;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddMongoDbStores<AppUser, AppRole, string>(connectionString, databaseName)
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Index";
    options.LogoutPath = "/Login/Logout";
    options.AccessDeniedPath = "/Login/Index";
});

builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();