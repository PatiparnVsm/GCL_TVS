using System;
using System.Collections.Generic;
using System.Reflection;

namespace GCL_TVS_API.Helper
{
    public class Util
    {
        public object ConvertByteArrayToBase64String(object obj,object objOutput)
        {

            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                string typeName = propertyInfo.PropertyType.Name;
                if (typeName.Equals("Byte[]"))
                {
                    var byteValue = (byte[])propertyInfo.GetValue(obj, null);
                    if (byteValue != null && byteValue.Length > 0)
                    {
                        var b64String = Convert.ToBase64String(byteValue);
                    }
                }
            }
            return objOutput; 
        }

        T GetObject<T>(Dictionary<string, string> dict)
        {
            Type type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dict)
            {
                type.GetProperty(kv.Key).SetValue(obj, kv.Value);
            }
            return (T)obj;
        }
    }
}