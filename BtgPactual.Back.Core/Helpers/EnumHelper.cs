using System.Runtime.Serialization;

namespace BtgPactual.Back.Core.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumInfo<T>(this T item)
        {
            string enumMemberValue = string.Empty;

            if (item is not null)
            {
                var memberInfo = typeof(T).GetField(item.ToString()!);
                var enumMemberAttribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(memberInfo!, typeof(EnumMemberAttribute));
                enumMemberValue = enumMemberAttribute?.Value;
            }

            return enumMemberValue;
        }
    }
}
