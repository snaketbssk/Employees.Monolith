using Employees.Monolith.Configurations.Entities;
using Employees.Monolith.DataLayer.Models.Tables.Entities;
using Employees.Monolith.LogicLayer.Models.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using UAParser;

namespace Employees.Monolith.Api.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class UserAgentModel : IUserAgentModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string FamilyOS { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string MajorOS { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string FamilyUserAgent { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string MajorUserAgent { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string FamilyDevice { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string BrandDevice { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string ModelDevice { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string IpAddress { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpRequest"></param>
        public UserAgentModel(HttpRequest httpRequest)
        {
            if (httpRequest == null) throw new ArgumentNullException(nameof(httpRequest));
            if (httpRequest.Headers.ContainsKey(HeaderConstant.X_FORWARDED_FOR))
            {
                IpAddress = httpRequest.Headers[HeaderConstant.X_FORWARDED_FOR].ToString();
            }
            else
            {
                IpAddress = httpRequest.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
            }
            if (httpRequest.Headers.ContainsKey(HeaderConstant.USER_AGENT))
            {
                var userAgent = httpRequest.Headers[HeaderConstant.USER_AGENT].ToString();
                var uaParser = Parser.GetDefault();
                var client = uaParser.Parse(userAgent);
                //
                FamilyOS = client.OS.Family;
                MajorOS = client.OS.Major;
                FamilyUserAgent = client.UA.Family;
                MajorUserAgent = client.UA.Major;
                FamilyDevice = client.Device.Family;
                BrandDevice = client.Device.Brand;
                ModelDevice = client.Device.Model;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public TokenTable Update(TokenTable result)
        {
            var nowAt = DateTime.UtcNow;
            result.FamilyOS = FamilyOS;
            result.MajorOS = MajorOS;
            result.FamilyUserAgent = FamilyUserAgent;
            result.MajorUserAgent = MajorUserAgent;
            result.FamilyDevice = FamilyDevice;
            result.BrandDevice = BrandDevice;
            result.ModelDevice = ModelDevice;
            result.IpAddress = IpAddress;
            result.UpdateAt = nowAt;
            var expiredAt = result.ExpiredAt - nowAt;
            if (expiredAt.Days < AppSettingsConfiguration.Instance.Root.Authentications.Token.ExpiredAt)
            {
                result.ExpiredAt = nowAt + TimeSpan.FromDays(AppSettingsConfiguration.Instance.Root.Authentications.Token.ExpiredAt);
            }
            return result;
        }
    }
}
