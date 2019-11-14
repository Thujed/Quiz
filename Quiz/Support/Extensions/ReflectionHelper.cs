using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Quiz.Support.Extensions
{
    static class ReflectionHelper
    {
        public static PropertyType GetElementDependencyPropertyValue<PropertyType>(this DependencyObject element, string propertyName) 
        {
            return (PropertyType)element.GetValue(element.GetFrameworkElementDependencyProperty(propertyName));
        }


        public static DependencyProperty GetFrameworkElementDependencyProperty(this DependencyObject element, string propertyName)
        {
            List<string> propertyNamesToCheck = new List<string> { propertyName, propertyName + "Property" };
            Type elementType = element.GetType();

            return propertyNamesToCheck.Select(s => elementType.GetPublicStaticField(s)).
                                        Where(f => f != null).
                                        Select(f => (DependencyProperty)f.GetValue(element)).
                                        FirstOrDefault();
        }

        public static FieldInfo GetPublicStaticField(this Type type, string fieldName)
        {
            return type.GetField(fieldName, BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static);
        }
    }
}
