using Models;
using Server;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:5050");

builder.Services.AddSingleton<ISampleService, SampleService>();

// Register OpenAPI/Swagger for development exploration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// GET /ping?s=hello
app.MapGet("/ping", (string s, ISampleService svc) =>
    Results.Ok(svc.Ping(s)))
    .WithName("Ping")
    .WithSummary("Echo a string back to the caller.");

// POST /ping-complex
app.MapPost("/ping-complex", (ComplexModelInput input, ISampleService svc) =>
    Results.Ok(svc.PingComplexModel(input)))
    .WithName("PingComplexModel")
    .WithSummary("Echo a complex model with some transformations.");

// GET /int-array
app.MapGet("/int-array", (ISampleService svc) =>
    Results.Ok(svc.IntArray()))
    .WithName("IntArray")
    .WithSummary("Return a fixed array of integers.");

// GET /complex-return-model
app.MapGet("/complex-return-model", (ISampleService svc) =>
    Results.Ok(svc.ComplexReturnModel()))
    .WithName("ComplexReturnModel")
    .WithSummary("Return an array of complex objects.");

// GET /void-method  (replaces void VoidMethod(out string s))
app.MapGet("/void-method", (ISampleService svc) =>
    Results.Ok(svc.VoidMethod()))
    .WithName("VoidMethod")
    .WithSummary("Returns a value that was previously an out parameter.");

// GET /async-method
app.MapGet("/async-method", async (ISampleService svc) =>
    Results.Ok(await svc.AsyncMethod()))
    .WithName("AsyncMethod")
    .WithSummary("Async operation that returns 42.");

// GET /nullable-method?arg=true
app.MapGet("/nullable-method", (bool? arg, ISampleService svc) =>
    Results.Ok(svc.NullableMethod(arg)))
    .WithName("NullableMethod")
    .WithSummary("Accepts a nullable bool and returns a nullable int.");

// POST /xml-method  (body is plain XML string)
app.MapPost("/xml-method", async (HttpRequest request, ISampleService svc) =>
{
    using var reader = new StreamReader(request.Body);
    var xml = await reader.ReadToEndAsync();
    svc.XmlMethod(xml);
    return Results.NoContent();
})
.WithName("XmlMethod")
.WithSummary("Accepts a raw XML string in the request body.")
.Accepts<string>("application/xml");

app.Run();
