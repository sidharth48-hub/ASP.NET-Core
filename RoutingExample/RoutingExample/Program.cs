var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.Map("map1", async (context) =>
    {
        await context.Response.WriteAsync("In map 1");
    });

    endpoints.MapGet("get1", async (context) =>
    {
        await context.Response.WriteAsync("Done Get Request");
    });

    endpoints.MapPost("post1", async (context) =>
    {
        await context.Response.WriteAsync("Done Post Request");
    });
});

app.Run();
