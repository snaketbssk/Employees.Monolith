using System.Linq;

namespace Employees.Monolith.LogicLayer.Extensions
{
    public static class StringExtension
    {
        public static string ToPhoneFormat(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var result = new string(value.Where(char.IsDigit).ToArray());
                return result;
            }
            return value;
        }
    }
}
