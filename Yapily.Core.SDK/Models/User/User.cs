using System;
using System.Collections.Generic;

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
}
