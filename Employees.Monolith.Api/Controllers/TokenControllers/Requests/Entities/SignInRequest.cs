using SnakeTBs.Extensions.Encryptors.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Employees.Monolith.Api.Controllers.TokenControllers.Requests.Entities
{
    public class SignInRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [JsonPropertyName("loginOrEmail")]
        [StringLength(256)]
        [DefaultValue("admin")]
        public string LoginOrEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private string _password;
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [JsonPropertyName("password")]
        [StringLength(256)]
        [DefaultValue("admin")]
        public string Password
        {
            get { return _password; }
            set { _password = MD5Encryptor.Encrypt(value); }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("codeTFA")]
        [StringLength(256)]
        [DefaultValue("")]
        public string CodeTFA { get; set; }
    }
}
