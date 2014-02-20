using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplHelpers
{
    public static class CollectionsHelpers
    {
        /// <summary>
        /// Serializes enum to key => value structure where value of the enum is the key
        /// It will disregard duplicate values
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static Dictionary<int, string> EnumToDictionary(Enum @enum)
        {
            var type = @enum.GetType();
            return Enum.GetValues(type).Cast<int>().Distinct().ToDictionary(e => e, e => Enum.GetName(type, e));
        }
    }
}
