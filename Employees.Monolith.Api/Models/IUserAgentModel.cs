namespace Employees.Monolith.Api.Models
{
    public interface IUserAgentModel
    {
        string FamilyOS { get; }
        string MajorOS { get; }
        string FamilyUserAgent { get; }
        string MajorUserAgent { get; }
        string FamilyDevice { get; }
        string BrandDevice { get; }
        string ModelDevice { get; }
        string IpAddress { get; }
    }
}
