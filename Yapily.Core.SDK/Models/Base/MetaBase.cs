namespace Yapily.Core.SDK.Models.Base
{
    public class Meta
    {
        public string TracingId { get; set; }
    }

    public class MetaAccounts : Meta
    {
        public int Count { get; set; }
    }

    public class MetaTransaction : MetaAccounts
    {
        public Pagination Pagination { get; set; }
    }
}
