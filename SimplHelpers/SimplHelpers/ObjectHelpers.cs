using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SimplHelpers
{
    public class ObjectHelpers
    {
        public static K CopyObject<T, K>(T source, K destination, bool onlyPrimitiveTypes, params string[] ignoreProperties) 
        {
            var sourceType = typeof(T);
            var destType = typeof(K);
            var destProperties = destType.GetProperties().ToArray();
            foreach (PropertyInfo sourceProperty in sourceType.GetProperties()) 
            {
                if (ignoreProperties.Contains(sourceProperty.Name))
                    continue;

                var destProperty = destProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);

                if (destProperty == null)
                    continue;

                if (destProperty.PropertyType != sourceProperty.PropertyType)
                    continue;                

                object sourceValue = sourceProperty.GetValue(source);
                if (sourceValue == null)
                    continue;

                if (onlyPrimitiveTypes && sourceProperty.PropertyType.IsClass)
                    continue;

                var setMethod = destProperty.GetSetMethod();
                if (setMethod != null)
                    destProperty.SetValue(destination, sourceValue);
            }
            return destination;
        }
    }
}
