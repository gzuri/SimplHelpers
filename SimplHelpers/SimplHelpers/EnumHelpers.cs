using System;
using System.ComponentModel;

namespace SimplHelpers
{
    public static class EnumHelpers
    {
        public static string GetEnumDescription(Enum value)
        {
            DescriptionAttribute[] descriptionAttributeArray = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (descriptionAttributeArray != null && descriptionAttributeArray.Length > 0)
                return descriptionAttributeArray[0].Description;
            return value.ToString();
        }
    }
}
