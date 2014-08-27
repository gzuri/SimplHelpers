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

                if (onlyPrimitiveTypes && sourceProperty.PropertyType.IsClass && sourceProperty.PropertyType != typeof(string))
                    continue;

                var setMethod = destProperty.GetSetMethod();
                if (setMethod != null)
                    destProperty.SetValue(destination, sourceValue);
            }
            return destination;
        }


        public static List<Tuple<string, Tuple<string, string>>> CompareObject<S, D>(S source, D destination, bool ignoreMissingProperties, params string[] ignoreProperties) 
        {
            var sourceType = typeof(S);
            var destType = typeof(D);
            var sourceProperties = sourceType.GetProperties().ToArray();
            var destProperties = destType.GetProperties().ToArray();
            var returnData = new List<Tuple<string, Tuple<string, string>>>();
            foreach (PropertyInfo sourceProperty in sourceType.GetProperties())
            {
                if (ignoreProperties.Contains(sourceProperty.Name))
                    continue;
                
                var destProperty = destProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);

                object sourceValue = sourceProperty.GetValue(source);
                if (destProperty == null) 
                {
                    if (!ignoreMissingProperties)
                        returnData.Add(Tuple.Create(sourceProperty.Name, Tuple.Create(sourceValue.ToString(), "null")));
                    continue;
                }

                if (sourceProperty.PropertyType.IsClass && sourceProperty.PropertyType != typeof(string))
                    continue;

                object destValue = destProperty.GetValue(destination);

                if (sourceValue == null && destValue == null)
                    continue;
                else if (sourceValue == null) 
                {
                    returnData.Add(Tuple.Create(sourceProperty.Name, Tuple.Create("null", destValue.ToString())));
                    continue;
                }
                else if (destValue == null) 
                {
                    returnData.Add(Tuple.Create(sourceProperty.Name, Tuple.Create(sourceValue.ToString(), "null")));
                    continue;
                }

                if (sourceValue.ToString() != destValue.ToString()) 
                {
                    returnData.Add(Tuple.Create(sourceProperty.Name, Tuple.Create(sourceValue.ToString(), destValue.ToString())));
                    continue;
                }
            }
            return returnData;
        }
    }
}
