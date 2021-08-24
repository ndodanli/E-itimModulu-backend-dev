using System;
using System.Collections.Generic;
using System.Reflection;

namespace DataAccess
{
    public class UtilityMethods
    {
        ///<summary>
        ///Aynı tipteki iki obje için, newObject'teki null ya da -1 olmayan degerleri oldObject'e atar.
        ///</summary>
        public static void UpdateProps<T>(T oldObject, T newObject)
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                var prop = property.GetValue(newObject, null);
                if (property.CanWrite)
                {
                    bool isIntOrEnum = property.PropertyType == typeof(int) || property.PropertyType.IsEnum;
                    if (prop != null && (isIntOrEnum ? (int)prop != -1 : true))
                    {
                        property.SetValue(oldObject, property.GetValue(newObject, null), null);
                    }
                }
            }
        }
    }
}
