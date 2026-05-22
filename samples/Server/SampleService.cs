using Models;

namespace Server;

public class SampleService : ISampleService
{
    public string Ping(string s)
    {
        Console.WriteLine("Exec ping method");
        return s;
    }

    public ComplexModelResponse PingComplexModel(ComplexModelInput inputModel)
    {
        Console.WriteLine("Input data. IntProperty: {0}, StringProperty: {1}",
            inputModel.IntProperty, inputModel.StringProperty);

        return new ComplexModelResponse
        {
            FloatProperty = float.MaxValue / 2,
            StringProperty = inputModel.StringProperty,
            ListProperty = inputModel.ListProperty,
            DateTimeOffsetProperty = inputModel.DateTimeOffsetProperty
        };
    }

    public int[] IntArray() => [123, 456, 789];

    public ComplexReturnModel[] ComplexReturnModel() =>
    [
        new ComplexReturnModel { Id = 1, Name = "Item 1" },
        new ComplexReturnModel { Id = 2, Name = "Item 2" }
    ];

    public VoidMethodResponse VoidMethod() =>
        new VoidMethodResponse { Value = "Value from server" };

    public Task<int> AsyncMethod() => Task.Run(() => 42);

    public int? NullableMethod(bool? arg) => null;

    public void XmlMethod(string xml) => Console.WriteLine(xml);
}
