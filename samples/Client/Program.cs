using System.Net.Http.Json;
using System.Text;
using Models;

var baseUrl = $"http://{Environment.MachineName}:5050";
using var http = new HttpClient { BaseAddress = new Uri(baseUrl) };

// --- Ping ---
var pingResult = await http.GetStringAsync($"/ping?s=hey");
Console.WriteLine("Ping result: {0}", pingResult.Trim('"'));

// --- PingComplexModel ---
var complexInput = new ComplexModelInput
{
    StringProperty = Guid.NewGuid().ToString(),
    IntProperty = int.MaxValue / 2,
    ListProperty = ["test", "list", "of", "strings"],
    DateTimeOffsetProperty = new DateTimeOffset(2018, 12, 31, 13, 59, 59, TimeSpan.FromHours(1))
};

var complexResult = await http.PostAsJsonAsync("/ping-complex", complexInput);
complexResult.EnsureSuccessStatusCode();
var complexResponse = await complexResult.Content.ReadFromJsonAsync<ComplexModelResponse>();
Console.WriteLine(
    "PingComplexModel result. FloatProperty: {0}, StringProperty: {1}, ListProperty: {2}, DateTimeOffsetProperty: {3}, EnumProperty: {4}",
    complexResponse!.FloatProperty,
    complexResponse.StringProperty,
    string.Join(", ", complexResponse.ListProperty ?? []),
    complexResponse.DateTimeOffsetProperty,
    complexResponse.TestEnum);

// --- VoidMethod (was out parameter, now returns a response object) ---
var voidResponse = await http.GetFromJsonAsync<VoidMethodResponse>("/void-method");
Console.WriteLine("Void method result: {0}", voidResponse!.Value);

// --- AsyncMethod ---
var asyncResult = await http.GetFromJsonAsync<int>("/async-method");
Console.WriteLine("Async method result: {0}", asyncResult);

// --- XmlMethod ---
var xmlContent = new StringContent("<test>string</test>", Encoding.UTF8, "application/xml");
var xmlResponse = await http.PostAsync("/xml-method", xmlContent);
xmlResponse.EnsureSuccessStatusCode();
Console.WriteLine("XmlMethod completed.");

Console.ReadKey();
