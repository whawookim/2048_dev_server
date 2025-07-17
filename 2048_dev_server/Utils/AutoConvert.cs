using System.Collections;
using System.Reflection;

public static class AutoConvert
{
    public static Dictionary<string, object> ToDictionary(object obj)
    {
        var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
        return ConvertToDictionary(obj, visited);
    }

    private static Dictionary<string, object> ConvertToDictionary(object obj, HashSet<object> visited)
    {
        if (obj == null)
            return null;

        if (visited.Contains(obj))
            return null; // 순환 방지

        visited.Add(obj);

        var dict = new Dictionary<string, object>();
        var type = obj.GetType();

        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!prop.CanRead)
                continue;

            var value = prop.GetValue(obj);

            if (value == null)
            {
                dict[prop.Name] = null;
            }
            else if (value is string || value.GetType().IsValueType)
            {
                dict[prop.Name] = value;
            }
            else if (value is IEnumerable enumerable && !(value is string))
            {
                var list = new List<object>();
                foreach (var item in enumerable)
                {
                    if (item == null || visited.Contains(item))
                        list.Add(null);
                    else if (item.GetType().IsValueType || item is string)
                        list.Add(item);
                    else
                        list.Add(ConvertToDictionary(item, visited));
                }
                dict[prop.Name] = list;
            }
            else
            {
                var nested = ConvertToDictionary(value, visited);
                dict[prop.Name] = nested;
            }
        }

        visited.Remove(obj); // 재귀 백트래킹 시 해제
        return dict;
    }
    
    public class ReferenceEqualityComparer : IEqualityComparer<object>
    {
        public static readonly ReferenceEqualityComparer Instance = new();

        public bool Equals(object x, object y) => ReferenceEquals(x, y);
        public int GetHashCode(object obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
    }
}