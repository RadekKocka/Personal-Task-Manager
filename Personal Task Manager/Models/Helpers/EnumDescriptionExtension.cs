using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Task_Manager.Models.Helpers
{
    public static class EnumDescriptionExtension
    {
        public static string GetDescription<T>(this T enumValue) where T : Enum
        {
            if (enumValue is null)
                return string.Empty;

            var member = typeof(T).GetMember(enumValue.ToString()).FirstOrDefault();
            if (member == null)
                return enumValue.ToString() ?? string.Empty;

            var attr = member.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? enumValue.ToString() ?? string.Empty;
        }

        public static KeyValuePair<T, string> GetValueAndDescription<T>(this T enumValue) where T : Enum
        {
            return new KeyValuePair<T, string>(enumValue, enumValue.GetDescription());
        }
    }
}
