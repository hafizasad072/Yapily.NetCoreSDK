namespace Yapily.Core.SDK.Models
{
    using System;
    using System.Collections.Generic;

    public class Transaction
    {
        public MetaTransaction Meta { get; set; }
        public List<TransactionData> Data { get; set; }
        public Links Links { get; set; }
    }

    public class MetaTransaction : MetaAccounts
    {
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public int TotalCount { get; set; }
        public Self Self { get; set; }
    }

    public class Self
    {
        public int Limit { get; set; }
        public string Sort { get; set; }
        public int Offset { get; set; }
    }

    public class TransactionData
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime BookingDateTime { get; set; }
        public DateTime ValueDateTime { get; set; }
        public string Status { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public TransactionAmount TransactionAmount { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public List<string> TransactionInformation { get; set; }
        public IsoBankTransactionCode IsoBankTransactionCode { get; set; }
        public ProprietaryBankTransactionCode ProprietaryBankTransactionCode { get; set; }
        public BalanceTransaction Balance { get; set; }
        public Enrichment Enrichment { get; set; }
    }

    public class TransactionAmount
    {
        public double Amount { get; set; }
        public string Currency { get; set; }
    }

    public class IsoBankTransactionCode
    {
        public DomainCode DomainCode { get; set; }
        public FamilyCode FamilyCode { get; set; }
        public SubFamilyCode SubFamilyCode { get; set; }
    }

    public class DomainCode
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class FamilyCode
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class SubFamilyCode
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class ProprietaryBankTransactionCode
    {
        public string Code { get; set; }
        public string Issuer { get; set; }
    }

    public class BalanceTransaction
    {
        public string Type { get; set; }
        public BalanceAmount BalanceAmount { get; set; }
    }

    public class Enrichment
    {
        public TransactionHash TransactionHash { get; set; }
    }

    public class TransactionHash
    {
        public string Hash { get; set; }
    }

    public class Links
    {
        public string Self { get; set; }
    }

}
