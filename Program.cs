using System.Globalization;
using ControllerExamples.CustomConstratint;
using ControllerExamples.ServiceContracts;
using ControllerExamples.ServiceExtensions;
using ControllerExamples.Services;

var builder = WebApplication.CreateBuilder(args);

/*
builder.Services.AddRouting(options =>
    options.ConstraintMap.Add("months", typeof(MonthCustomConstraint)));
*/

builder.Services.Add(new ServiceDescriptor(
    typeof(ICityServices),
    typeof(CityServices),
    ServiceLifetime.Transient
));

builder.Services.AddControllersWithViews(); // adds all the controller classes as services
//builder.Services.AddMemoryCache();
//builder.Services.AddSession();
builder.Services.ConfigureSession();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.MapControllers();
/*
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/employee/profile/{employeename:minlength(3)=prashant}", async (context) =>
    {
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);

        if (string.IsNullOrEmpty(employeeName))
        {
            await context?.Response?.WriteAsync("No value supplied")!;
        }
        else
        {
            await context.Response.WriteAsync($"Employee Name: {employeeName}");
        }
    });

    endpoints.MapGet("/student/profile/{studentname:length(3, 10):alpha=prashant}", async (context) =>
    {
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);

        if (string.IsNullOrEmpty(employeeName))
        {
            await context?.Response?.WriteAsync("No value supplied")!;
        }
        else
        {
            await context.Response.WriteAsync($"Employee Name: {employeeName}");
        }
    });

    endpoints.Map("sales-report/{year:int:min(1900)}/{month:regex(^(apr|jul|oct|jan)$)}", async context =>
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = textInfo.ToTitleCase(Convert.ToString(context.Request.RouteValues["month"]));
        await context.Response.WriteAsync($"<h1>Sales Report for - {month}-{year}</h1>");
    });

    endpoints.Map("financial-years/{month:months}", async context =>
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        string? month = textInfo.ToTitleCase(Convert.ToString(context.Request.RouteValues["month"]));
        await context.Response.WriteAsync($"<h1>Month: {month}</h1>");

    });
});

app.Run(async (context) =>
{
    await context.Response.WriteAsync($"<h1>No route found for path: {context.Request.Path}</h1>");
});
*/
app.Run();

