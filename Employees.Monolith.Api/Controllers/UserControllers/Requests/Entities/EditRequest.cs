using Employees.Monolith.Api.Authentications.Models.Constants;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Employees.Monolith.Api.Controllers.UserControllers.Requests.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class EditRequest
    {
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
        public string Phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public UserTable Update(UserTable result)
        {
            result.FirstName = FirstName;
            result.MiddleName = MiddleName;
            result.LastName = LastName;
            result.Phone = Phone.ToPhoneFormat();
            result.UpdateAt = DateTime.UtcNow;
            return result;
        }
    }
}
