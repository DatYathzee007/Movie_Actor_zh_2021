using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Actor_zh_2021.App
{
    //HELPER CLASS (4 PONT)
    public static class Validator
    {
        //a)	Create a Validator class that can validate a certain property if it has StringRangeAttribute on it.
        //      It should contain a method called IsValid<T>(string propertyName), where T is the class whose property you want to check
        //      and propertyName is the name of the property.
        //b)	The method should return a boolean value, which should be true if the valid values contain the property’s value, it should return false otherwise.
        //c)	In case the property being validated does not exist or does not have the attribute on it, the method should throw an ArgumentException.

        public static bool IsValid<T>(this T instance, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or whitespace.", nameof(propertyName));
            }
            //var markedProperties = typeof(T).GetProperties().Where(propInfo => propInfo.GetCustomAttributes(typeof(StringRangeAttribute), false).Length > 0);

            PropertyInfo requestedProperty = typeof(T).GetProperty(propertyName);
            if (requestedProperty is null)
            {
                throw new ArgumentException($"'{nameof(requestedProperty)}' cannot be null. The {nameof(T)} class should have the property you specify.", nameof(requestedProperty));
            }

            StringRangeAttribute strRangeAttr = requestedProperty.GetCustomAttribute<StringRangeAttribute>();
            if (strRangeAttr is null)
            {
                throw new ArgumentException($"'{nameof(strRangeAttr)}' cannot be null. The {nameof(T)} class does have the member (property) you specified but {nameof(StringRangeAttribute)} was not found on this property.", nameof(strRangeAttr));
            }

            return strRangeAttr.WhiteList.Contains(requestedProperty.GetValue(instance).ToString());
        }
    }
}
