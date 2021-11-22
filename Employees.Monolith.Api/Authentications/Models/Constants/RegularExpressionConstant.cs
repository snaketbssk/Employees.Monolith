namespace Employees.Monolith.Api.Authentications.Models.Constants
{
    public static class RegularExpressionConstant
    {
        public const string LOGIN = @"^[\w.-]{0,19}[0-9a-zA-Z]$";
        public const string PHONE = @"(([+][(]?[0-9]{1,3}[)]?)|([(]?[0-9]{4}[)]?))\s*[)]?[-\s.]?[(]?[0-9]{1,3}[)]?([-\s.]?[0-9]{3})([-\s.]?[0-9]{3,4})";
    }
}
