namespace Yapily.Core.SDK.Models
{
    using System.Collections.Generic;

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

    public class Country
    {
        public string DisplayName { get; set; }
        public string CountryCode2 { get; set; }
    }

    public class Media
    {
        public string Source { get; set; }
        public string Type { get; set; }
    }

}
