﻿using CommercialRecordSystem.Common;
using CommercialRecordSystem.Models.Accounts;
using System;
using System.Linq;

namespace CommercialRecordSystem.ViewModels.DataVMs.Accounts
{
    class CurrentAccountVM : AccountBaseVM<CurrentAccount>
    {
        private int actorId = 0;
        public int ActorId {
            get
            {
                return actorId;
            }
            set 
            {
                actorId = value;
                RaisePropertyChanged("ActorId", false);
            }
        }

        public CurrentAccountVM() 
        {
            Type = 0;
            Dirty = false;
        }
    }
}
