using System.Security.Claims;

namespace Employees.Monolith.Api.Authentications.Models.Constants
{
    public static class ClaimConstant
    {
        public const string ID = ClaimTypes.NameIdentifier;
        public const string LOGIN = ClaimTypes.Name;
        public const string EMAIL = ClaimTypes.Email;
        public const string ROLE = ClaimTypes.Role;
        public const string LANGUAGE = "Language";
        public const string CONFIRM_EMAIL = "ConfirmEmail";
        public const string CONFIRM_TFA = "ConfirmTFA";
        public const string GUID = "Guid";
        public const string X_TOKEN = "X-Token";
    }
}
