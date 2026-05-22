namespace Models;

/// <summary>
/// Service contract for the sample REST API.
/// </summary>
public interface ISampleService
{
    string Ping(string s);
    ComplexModelResponse PingComplexModel(ComplexModelInput inputModel);
    int[] IntArray();
    ComplexReturnModel[] ComplexReturnModel();
    VoidMethodResponse VoidMethod();
    Task<int> AsyncMethod();
    int? NullableMethod(bool? arg);
    void XmlMethod(string xml);
}

/// <summary>
/// Response wrapper for VoidMethod (replaces the out parameter).
/// </summary>
public class VoidMethodResponse
{
    public string? Value { get; set; }
}
