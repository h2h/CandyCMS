using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using Candy.Framework.ComponentModel;

namespace Candy.Framework
{
    public partial class CommonHelper
    {
        private static AspNetHostingPermissionLevel? _trustLevel;
        public static AspNetHostingPermissionLevel GetTrustLevel()
        {
            if (!_trustLevel.HasValue)
            {
                foreach (var trustLevel in new[]{
                    AspNetHostingPermissionLevel.Unrestricted,
                    AspNetHostingPermissionLevel.High,
                    AspNetHostingPermissionLevel.Medium,
                    AspNetHostingPermissionLevel.Low,
                    AspNetHostingPermissionLevel.Minimal
                })
                {
                    try
                    {
                        new AspNetHostingPermission(trustLevel).Demand();
                        _trustLevel = trustLevel;
                        break;
                    }
                    catch (System.Security.SecurityException)
                    {
                        continue;
                    }
                }
            }

            return _trustLevel.Value;
        }

        public static TypeConverter GetCustomTypeConverter(Type type)
        {
            if (type == typeof(List<int>))
                return new GenericListTypeConverter<int>();

            if (type == typeof(List<decimal>))
                return new GenericListTypeConverter<decimal>();

            if (type == typeof(List<string>))
                return new GenericListTypeConverter<string>();

            return TypeDescriptor.GetConverter(type);
        }
        public static object To(object value, Type destinationType)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                var destinationConverter = GetCustomTypeConverter(destinationType);
                var sourceConverter = GetCustomTypeConverter(sourceType);

                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(value);

                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(value, destinationType);

                if (destinationType.IsEnum && value is int)
                    return Enum.ToObject(destinationType, (int)value);

                if (!destinationType.IsInstanceOfType(value))
                    return Convert.ChangeType(value, destinationType);
            }
            return value;
        }

        public static T To<T>(object value)
        {
            return (T)To(value, typeof(T));
        }

    }
}
