namespace Models;

public class ComplexModelResponse
{
    public float FloatProperty { get; set; }
    public string? StringProperty { get; set; }
    public List<string>? ListProperty { get; set; }
    public DateTimeOffset DateTimeOffsetProperty { get; set; }
    public TestEnum TestEnum { get; set; }
}

public enum TestEnum
{
    One,
    Two
}
