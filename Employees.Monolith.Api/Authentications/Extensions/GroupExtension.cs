using Employees.Monolith.Api.Authentications.Models.Constants;
using System;

namespace Employees.Monolith.Api.Authentications.Extensions
{
    public static class GroupExtension
    {
        public static string GetString(this GroupAuthentication roleUser)
        {
            switch (roleUser)
            {
                case GroupAuthentication.Administrators: return GroupConstant.ADMINISTRATORS;
                case GroupAuthentication.Service: return GroupConstant.SERVICE;
                case GroupAuthentication.Admin: return GroupConstant.ADMIN;
                case GroupAuthentication.User: return GroupConstant.USER;
                case GroupAuthentication.Bot: return GroupConstant.BOT;
                case GroupAuthentication.Identity: return GroupConstant.IDENTITY;
                case GroupAuthentication.Human: return GroupConstant.HUMAN;
                default: throw new ArgumentException(nameof(roleUser));
            }
        }
    }
}
