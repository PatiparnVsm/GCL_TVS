using GCL_TVS_API.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GCL_TVS_API.Helper
{
    public class Util
    {
        public object ConvertByteArrayToBase64String(object obj, object objOutput)
        {
            try
            {
                foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
                {
                    string typeName = propertyInfo.PropertyType.Name;
                    Type t = propertyInfo.GetType();
                    string Name = propertyInfo.Name;
                    //var v = propertyInfo.GetValue(obj);

                    //if (typeName.Equals("Byte[]"))
                    //{
                    //    var byteValue = (byte[])propertyInfo.GetValue(obj, null);
                    //    if (byteValue != null && byteValue.Length > 0)
                    //    {
                    //        v = Convert.ToBase64String(byteValue);
                    //    }
                    //}
                    
                    objOutput.GetType().GetField(Name).SetValue(objOutput, new Guid());
                    //foreach (PropertyInfo pInfo in objOutput.GetType().GetProperties().)
                    //{
                    //    string outName = pInfo.Name;
                    //    if (Name == outName)
                    //    {
                    //        pInfo.SetValue(objOutput, v);
                    //    }
                    //}

                }
            }
            catch(Exception ex)
            {

            }
            return objOutput;
        }

        public T GetObject<T>(Dictionary<string, string> dict)
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