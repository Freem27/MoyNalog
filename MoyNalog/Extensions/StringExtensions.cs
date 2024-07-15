namespace MoyNalog.Extensions;

public static class StringExtensions
{
    public static string ToCamelCase(this string value)
    {
        if(value.Length <= 1)
        {
            return value.ToLower();
        }
        return $"{value[0].ToString().ToLower()}{value.Substring(1)}";
    }
}
