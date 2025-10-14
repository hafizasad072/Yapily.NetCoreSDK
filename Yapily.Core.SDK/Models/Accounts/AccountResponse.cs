using System;
using System.Collections.Generic;
using Yapily.Core.SDK.Models.Base;

namespace Yapily.Core.SDK.Models
{
    public class AccountResponce
    {
        public Meta Meta { get; set; }
        public ConsentData Data { get; set; }
    }

    public class ConsentData
    {
        public string Id { get; set; }
        public string UserUuid { get; set; }
        public string ApplicationUserId { get; set; }
        public string InstitutionId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> FeatureScope { get; set; }
        public string State { get; set; }
        public string InstitutionConsentId { get; set; }
        public string AuthorisationUrl { get; set; }
        public string QrCodeUrl { get; set; }
    }
}
