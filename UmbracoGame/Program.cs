using UmbracoGame.Business.Services.Interfaces;
using UmbracoGame.Business;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IGameService, GameService>();

WebApplication app = builder.Build();

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
