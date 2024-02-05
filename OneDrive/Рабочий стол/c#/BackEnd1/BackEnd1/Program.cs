var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");



app.MapControllerRoute(
    name: "potom",
    pattern: "{controller=Home}/{action=Main}");

app.MapControllerRoute(
    name: "potom",
    pattern: "{controller=Home}/{action=About}");

app.MapControllerRoute(
    name: "potom",
    pattern: "{controller=Home}/{action=Contact}");


app.Run();