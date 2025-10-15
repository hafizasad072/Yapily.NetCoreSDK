using System;
using System.Collections.Generic;
using Yapily.BO.Models.Base;

namespace Yapily.BO.Models
{
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

    public class CreditLineAmount : AmountBase { }
}
