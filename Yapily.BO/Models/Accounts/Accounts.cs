using System;
using System.Collections.Generic;
using Yapily.BO.Models.Base;

namespace Yapily.BO.Models
{
    public class Accounts
    {
        public MetaAccounts Meta { get; set; }
        public List<AccountData> Data { get; set; }
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

    public class BalanceAmount : Yapily.BO.Models.Base.AmountBase { }
}
