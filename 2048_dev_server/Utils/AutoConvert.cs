using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class AutoConvert
{
    public static Dictionary<string, object> ToDictionary(object obj)
    {
        if (obj == null)
            return new Dictionary<string, object>();

        var dict = new Dictionary<string, object>();
        var props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in props)
        {
            var value = prop.GetValue(obj);

            if (value == null)
                continue;

            if (value is string || value.GetType().IsValueType)
            {
                dict[prop.Name] = value;
            }
            else if (value is IEnumerable enumerable && !(value is string))
            {
                var list = new List<object>();
                foreach (var item in enumerable)
                {
                    if (item == null)
                        continue;

                    if (item.GetType().IsValueType || item is string)
                        list.Add(item);
                    else
                        list.Add(ToDictionary(item));
                }
                dict[prop.Name] = list;
            }
            else
            {
                dict[prop.Name] = ToDictionary(value);
            }
        }

        return dict;
    }
}
