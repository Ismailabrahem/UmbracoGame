using UmbracoGame.Business.Services.Interfaces;
using UmbracoGame.Business;
using UmbracoGame.Business.ScheduledJobs;
using UmbracoGame.Business.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

//builder.Services.AddHttpContextAccessor();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGamesJob, GamesJob>();
builder.Services.AddSingleton<ISitemapService, SitemapService>();
builder.Services.AddSingleton<IPetaPocoService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("umbracoDbDSN");
    return new PetaPocoService(connectionString);
});

WebApplication app = builder.Build();

app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == 404)
    {
        context.HttpContext.Response.Redirect("/error");
        await Task.Yield();
    }
});
//app.UseExceptionHandler("/error"); // Redirect to your custom error page for unhandled exceptions
//app.UseStatusCodePagesWithReExecute("/error"); // Redirect to your custom error page for status codes like 404
app.MapBlazorHub();

await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();

        //u.EndpointRouteBuilder.MapControllerRoute(
        //    name: "GameDetails",
        //    pattern: "game",
        //    defaults: new { controller = "Game", action = "Details" });
    });

await app.RunAsync();
