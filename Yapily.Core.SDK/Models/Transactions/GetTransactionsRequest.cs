namespace Yapily.Core.SDK.Models.Transactions
{
    public class GetTransactionsRequest
    {
        public string ConsentId { get; set; }
        public string AccountId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? Limit { get; set; }
        public string? Sort { get; set; }
        public int? Offset { get; set; }
        public string? Cursor { get; set; }
    }
}
