using RoutingExample.CustomConstraints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => {
    options.ConstraintMap.Add("months", typeof(MonthCustomConstraint));
});
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    //endpoints.Map("map1", async (context) =>
    //{
    //    await context.Response.WriteAsync("In map 1");
    //});

    //endpoints.MapGet("get1", async (context) =>
    //{
    //    await context.Response.WriteAsync("Done Get Request");
    //});

    //endpoints.MapPost("post1", async (context) =>
    //{
    //    await context.Response.WriteAsync("Done Post Request");
    //});

    //endpoints.Map("files/{filename}.{extension}", async (context) =>
    //{
    //    string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
    //    string? ext = Convert.ToString(context.Request.RouteValues["extension"]);

    //    if(fileName == "sample" && ext == "txt")
    //    {
    //        await context.Response.WriteAsync("Asked for sample.txt");
    //    }
    //});

    //endpoints.Map("product/details/{id:int?}", async (context) =>
    //{
    //    int id = Convert.ToInt32(context.Request.RouteValues["id"]);
    //    await context.Response.WriteAsync($"Product details - {id}");
    //});

    endpoints.Map("sales-report/{year:int:min(1900)}/{month:months}", async context =>
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);

        string? month = Convert.ToString(context.Request.RouteValues["month"]);

        if(month == "jan" || month == "jul" || month == "apr" || month == "oct")
        {
            await context.Response.WriteAsync($"Sales Report - {year} - {month}");
        }
    });

});

app.Run(async (context) =>
{
    await context.Response.WriteAsync($"Request recieved at {context.Request.Path}");
});

app.Run();
