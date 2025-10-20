namespace Yapily.BO.Models
{
    public class User
    {
        public string Uuid { get; set; }
        public string ApplicationUuid { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<object> InstitutionConsents { get; set; }
        public bool VopOptOut { get; set; }
    }

    public class UserCreateRequest
    {
        public string? ApplicationUserId { get; set; }
        public string? ReferenceId { get; set; }
        public bool? VopOptOut { get; set; }
    }

    public class UserUpdate
    {
        public string Op { get; set; }
        public string Path { get; set; }
        public string Value { get; set; }
    }
}
