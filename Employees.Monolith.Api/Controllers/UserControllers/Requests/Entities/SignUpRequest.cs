using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.DataLayer.Models.Tables;
using Employees.Monolith.LogicLayer.Models.Creaters.Entities;
using SnakeTBs.Extensions.Encryptors.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Employees.Monolith.Api.Controllers.UserControllers.Requests.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class SignUpRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [JsonPropertyName("login")]
        [RegularExpression(RegularExpressionConstant.LOGIN)]
        public string Login { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        [StringLength(256)]
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private string _password;
        /// <summary>
        /// 
        /// </summary>
        [DataType(DataType.Password)]
        [JsonPropertyName("password")]
        [StringLength(256)]
        public string Password
        {
            get { return _password; }
            set { _password = MD5Encryptor.Encrypt(value); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _confirmPassword;
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [JsonPropertyName("confirmPassword")]
        [StringLength(256)]
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = MD5Encryptor.Encrypt(value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("firstName")]
        [StringLength(256)]
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("middleName")]
        [StringLength(256)]
        public string MiddleName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("lastName")]
        [StringLength(256)]
        public string LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("phone")]
        [RegularExpression(RegularExpressionConstant.PHONE)]
        [DefaultValue("")]
        public string Phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public LanguageUserTable Language { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        public Guid? Referral { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public UserTableCreater ToUserTableCreater(RoleUserTable role)
        {
            var result = new UserTableCreater(
                login: Login,
                email: Email,
                password: Password,
                firstName: FirstName,
                middleName: MiddleName,
                lastName: LastName,
                phone: Phone,
                role: role,
                language: Language);
            return result;
        }
    }
}
