﻿using System;

namespace CommercialRecordSystem.Models.Accounts
{
    class AccountBase : ModelBase
    {
        public int Type { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Active { get; set; }
        public DateTime LastTransactDate { get; set; }
        public double TotalDebt { get; set; }
        public double TotalCredit { get; set; }
        public string Detail { get; set; }
    }
}
