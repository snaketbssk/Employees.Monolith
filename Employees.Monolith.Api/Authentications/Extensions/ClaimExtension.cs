using Employees.Monolith.Api.Authentications.Models.Constants;
using System;

namespace Employees.Monolith.Api.Authentications.Extensions
{
    public static class ClaimExtension
    {
        public static string GetString(this ClaimAuthentication roleUser)
        {
            switch (roleUser)
            {
                case ClaimAuthentication.Id: return ClaimConstant.ID;
                case ClaimAuthentication.Login: return ClaimConstant.LOGIN;
                case ClaimAuthentication.Email: return ClaimConstant.EMAIL;
                case ClaimAuthentication.Role: return ClaimConstant.ROLE;
                case ClaimAuthentication.Language: return ClaimConstant.LANGUAGE;
                case ClaimAuthentication.ConfirmEmail: return ClaimConstant.CONFIRM_EMAIL;
                case ClaimAuthentication.ConfirmTFA: return ClaimConstant.CONFIRM_TFA;
                case ClaimAuthentication.Guid: return ClaimConstant.GUID;
                case ClaimAuthentication.XToken: return ClaimConstant.X_TOKEN;
                default: throw new ArgumentException(nameof(roleUser));
            }
        }
    }
}
