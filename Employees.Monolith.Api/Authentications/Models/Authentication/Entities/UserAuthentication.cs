using Employees.Monolith.Api.Authentications.Extensions;
using Employees.Monolith.DataLayer.Models.Tables;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Employees.Monolith.Api.Authentications.Models.Authentication.Entities
{
    public class UserAuthentication : UserTable
    {
        public Guid Token { get; set; }
        public UserAuthentication(UserTable User, Guid guidToken)
        {
            Id = User.Id;
            Login = User.Login;
            Email = User.Email;
            Role = User.Role;
            Language = User.Language;
            ConfirmEmail = User.ConfirmEmail;
            ConfirmTFA = User.ConfirmTFA;
            Guid = User.Guid;
            Token = guidToken;
        }
        public UserAuthentication(TokenTable tokenTable)
        {
            Id = tokenTable.User.Id;
            Login = tokenTable.User.Login;
            Email = tokenTable.User.Email;
            Role = tokenTable.User.Role;
            Language = tokenTable.User.Language;
            ConfirmEmail = tokenTable.User.ConfirmEmail;
            ConfirmTFA = tokenTable.User.ConfirmTFA;
            Guid = tokenTable.User.Guid;
            Token = tokenTable.Guid.Value;
        }
        public UserAuthentication(ClaimsPrincipal claimsPrincipal)
        {
            foreach (ClaimAuthentication claimUser in Enum.GetValues(typeof(ClaimAuthentication)))
            {
                var text = claimUser.GetString();
                var claim = claimsPrincipal.FindFirst(text) ?? throw new ArgumentNullException(nameof(claimUser));
                switch (claimUser)
                {
                    case ClaimAuthentication.Id: Id = int.Parse(claim.Value); break;
                    case ClaimAuthentication.Login: Login = claim.Value; break;
                    case ClaimAuthentication.Email: Email = claim.Value; break;
                    case ClaimAuthentication.Role: Role = Enum.Parse<RoleUserTable>(claim.Value); break;
                    case ClaimAuthentication.Language: Language = Enum.Parse<LanguageUserTable>(claim.Value); break;
                    case ClaimAuthentication.ConfirmEmail: ConfirmEmail = bool.Parse(claim.Value); break;
                    case ClaimAuthentication.ConfirmTFA: ConfirmTFA = bool.Parse(claim.Value); break;
                    case ClaimAuthentication.Guid: Guid = System.Guid.Parse(claim.Value); break;
                    case ClaimAuthentication.XToken: Token = System.Guid.Parse(claim.Value); break;
                    default: throw new ArgumentException(nameof(claimUser));
                }
            }
        }
        public IEnumerable<Claim> GetClaims()
        {
            var result = new List<Claim>();
            foreach (ClaimAuthentication claimUser in Enum.GetValues(typeof(ClaimAuthentication)))
            {
                var text = claimUser.GetString();
                Claim claim = null;
                switch (claimUser)
                {
                    case ClaimAuthentication.Id: claim = new Claim(text, Id.ToString()); break;
                    case ClaimAuthentication.Login: claim = new Claim(text, Login); break;
                    case ClaimAuthentication.Email: claim = new Claim(text, Email); break;
                    case ClaimAuthentication.Role: claim = new Claim(text, Role.ToString()); break;
                    case ClaimAuthentication.Language: claim = new Claim(text, Language.ToString()); break;
                    case ClaimAuthentication.ConfirmEmail: claim = new Claim(text, ConfirmEmail.ToString()); break;
                    case ClaimAuthentication.ConfirmTFA: claim = new Claim(text, ConfirmTFA.ToString()); break;
                    case ClaimAuthentication.Guid: claim = new Claim(text, Guid.ToString()); break;
                    case ClaimAuthentication.XToken: claim = new Claim(text, Token.ToString()); break;
                    default: throw new ArgumentException(nameof(claimUser));
                }
                result.Add(claim);
            }
            return result;
        }
    }
}
