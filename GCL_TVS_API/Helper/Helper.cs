using GCL_TVS_API.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GCL_TVS_API.Helper
{
    public class HelperUtil
    {
        public object MapObjectByteArrayToObjectString(object obj, object objOutput)
        {
            try
            {
                foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
                {
                    string typeName = propertyInfo.PropertyType.Name;
                    Type t = propertyInfo.GetType();
                    string Name = propertyInfo.Name;
                    var v = propertyInfo.GetValue(obj);

                    if (typeName.Equals("Byte[]"))
                    {
                        var byteValue = (byte[])propertyInfo.GetValue(obj, null);
                        if (byteValue != null && byteValue.Length > 0)
                        {
                            v = Convert.ToBase64String(byteValue);
                        }
                    }

                    objOutput.GetType().GetProperty(Name).SetValue(objOutput, v);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objOutput;
        }

        public List<SODetails> GenerateSoDetailBase64String(List<SODetailsDB> listData)
        {
            List<SODetails> listResponse = new List<SODetails>();
            if (listData != null && listData.Count > 0)
            {
                for (var i = 0; i < listData.Count; i++)
                {
                    var sodetailObj = MapObjectByteArrayToObjectString(listData[i], new SODetails());
                    listResponse.Add((SODetails)sodetailObj);
                }
            }

            return listResponse;
        }
        public List<PictureList<string>> GeneratePictureListBase64String(List<PictureList<byte[]>> listData)
        {
            List<PictureList<string>> listResponse = new List<PictureList<string>>();
            if (listData != null && listData.Count > 0)
            {
                for (var i = 0; i < listData.Count; i++)
                {
                    var Obj = MapObjectByteArrayToObjectString(listData[i], new SODetails());
                    listResponse.Add((PictureList<string>)Obj);
                }
            }

            return listResponse;
        }
    }
}