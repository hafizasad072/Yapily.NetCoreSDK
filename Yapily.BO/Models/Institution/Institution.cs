using System.Collections.Generic;
using Yapily.BO.Models.Base;
using Yapily.BO.Models.Common;

namespace Yapily.BO.Models
{
    public class Institution
    {
        public MetaTransaction Meta { get; set; }
        public List<DataItem> Data { get; set; }
    }

    public class DataItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public List<Country> Countries { get; set; }
        public string EnvironmentType { get; set; }
        public string CredentialsType { get; set; }
        public List<Media> Media { get; set; }
        public List<string> Features { get; set; }
    }
}
