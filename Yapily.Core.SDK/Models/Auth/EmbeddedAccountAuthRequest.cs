using System.Collections.Generic;

namespace Yapily.BO.Models
{
    public class EmbeddedAccountAuthRequest
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string UserUuid { get; set; }
        public string InstitutionId { get; set; }
        public List<string> ScaMethods { get; set; }
        public string ConsentToken { get; set; }
    }

    public class EmbeddedAuthUpdate
    {
        public string ScaMethod { get; set; }
        public string ScaCode { get; set; }
    }

    public class Consent
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string ExpiryDateTime { get; set; }
        public string InstitutionId { get; set; }
        public string UserUuid { get; set; }
    }
}
