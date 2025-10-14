namespace Yapily.Core.SDK.Models
{
    public class User
    {
        public string Uuid { get; set; }
        public string ApplicationUuid { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<object> InstitutionConsents { get; set; }
        public bool VopOptOut { get; set; }
    }
    public class YapilyResponse<T>
    {
        public T Data { get; set; }
        // you can capture meta, tracingId, etc
    }
    public class InstitutionCountry
    {
        public string DisplayName { get; set; }
        public string CountryCode2 { get; set; }
    }

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
    #region Account

    public class AccountResponce
    {
        public Meta Meta { get; set; }
        public ConsentData Data { get; set; }
    }

    public class Meta
    {
        public string TracingId { get; set; }
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

    #endregion

    #region Accounts

    public class Accounts
    {
        public MetaAccounts Meta { get; set; }
        public List<AccountData> Data { get; set; }
    }

    public class MetaAccounts : Meta
    {
        public int Count { get; set; }
    }

    public class AccountData
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public string UsageType { get; set; }
        public string AccountType { get; set; }
        public string Nickname { get; set; }
        public List<AccountName> AccountNames { get; set; }
        public List<AccountIdentification> AccountIdentifications { get; set; }
        public List<AccountBalance> AccountBalances { get; set; }
    }

    public class AccountName
    {
        public string Name { get; set; }
    }

    public class AccountIdentification
    {
        public string Type { get; set; }
        public string Identification { get; set; }
    }

    public class AccountBalance
    {
        public string Type { get; set; }
        public DateTime DateTime { get; set; }
        public BalanceAmount BalanceAmount { get; set; }
        public bool CreditLineIncluded { get; set; }
        public List<object> CreditLines { get; set; }
    }

    public class BalanceAmount
    {
        public double Amount { get; set; }
        public string Currency { get; set; }
    }

    #endregion

    public class BalanceVM
    {
        public Meta Meta { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public BalanceAmount MainBalanceAmount { get; set; }
        public List<Balance> Balances { get; set; }
    }

    public class Balance
    {
        public string Type { get; set; }
        public DateTime DateTime { get; set; }
        public BalanceAmount BalanceAmount { get; set; }
        public bool CreditLineIncluded { get; set; }
        public List<CreditLine> CreditLines { get; set; }
    }

    public class CreditLine
    {
        public string Type { get; set; }
        public CreditLineAmount CreditLineAmount { get; set; }
    }

    public class CreditLineAmount
    {
        public double Amount { get; set; }
        public string Currency { get; set; }
    }


}
