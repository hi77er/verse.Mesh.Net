namespace verse.Mesh.Net.Core.Extensions;

public static class ObjectExtensions
{
  public static bool IsByteArray(this object val) => val is byte[];

  public static bool IsScalar(this object val)
    => val is bool || val is string || val is short || val is int || val is long || val is double || val is decimal;

}
