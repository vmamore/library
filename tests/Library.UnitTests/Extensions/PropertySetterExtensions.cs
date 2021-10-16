using System.Reflection;

public static class PropertySetterExtensions
{
    public static void Set(this object obj, string propertyName, object value)
    {
        var type = obj.GetType();
        var property = type.GetProperty(propertyName);
        property.SetValue(obj, value);
    }
}