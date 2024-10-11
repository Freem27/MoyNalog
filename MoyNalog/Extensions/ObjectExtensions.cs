using System.Reflection;
using System.Runtime.Serialization;
using System.Web;

namespace TDV.MoyNalog.Extensions;

public static class ObjectExtensions
{
    public static string ToQueryString(this object obj)
    {
        if (obj == null)
        {
            return "";
        }

        var properties = obj.GetType().GetProperties().Where(
            p => p.GetValue(obj, null) != null)
            .Select(p => {
                var val = p?.GetValue(obj);
                
                Type fieldType = val.GetType();
                if (fieldType == typeof(DateTime))
                {
                    val = ((DateTime)val).ToString("yyyy-MM-ddTHH:mm:ss.fffzzz").Replace("+", "%2B");
                }
                else if (fieldType.IsEnum)
                {
                    var props = fieldType.GetProperties();
                    var memberInfos = fieldType.GetMember(val?.ToString() ?? throw new NullReferenceException(nameof(val)));
                    var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == fieldType);
                    var customValue = enumValueMemberInfo?.GetCustomAttribute<EnumMemberAttribute>()?.Value;
                    if (customValue != null)
                    {
                        val = customValue;
                    }
                }
                else
                {
                    val = HttpUtility.UrlEncode(val?.ToString());
                }

                return p!.Name.ToCamelCase() + "=" + val;
            });
        var result = string.Join("&", properties.ToArray());
        return string.IsNullOrWhiteSpace(result) ? "" : "?" + result;
    }
}
