namespace Models;

public class ComplexModelInput
{
    public string? StringProperty { get; set; }
    public int IntProperty { get; set; }
    public List<string>? ListProperty { get; set; }
    public DateTimeOffset DateTimeOffsetProperty { get; set; }
    public List<ComplexObject>? ComplexListProperty { get; set; }
    public List<BaseObject>? DerivedObjects { get; set; }
}

public class ComplexObject
{
    public string? StringProperty { get; set; }
    public int IntProperty { get; set; }
}

public class BaseObject
{
    public string Name { get; set; } = nameof(BaseObject);
}

public class DerivedObject : BaseObject
{
    public DerivedObject()
    {
        Name = nameof(DerivedObject);
    }
}
