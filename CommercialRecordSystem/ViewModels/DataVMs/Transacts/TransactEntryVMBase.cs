﻿

using CommercialRecordSystem.Models;
using CommercialRecordSystem.ViewModels.DataVMs;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
namespace CommercialRecordSystem.ViewModels.Transacts
{
    abstract class TransactEntryVMBase<E> : DataVMBase<E> where E: ModelBase, new()
    {
        #region Properties
        private int transactId = 0;
        public int TransactId
        {
            get
            {
                return transactId;
            }
            set
            {
                transactId = value;
                RaisePropertyChanged("TransactId");
            }
        }

        private string detail = string.Empty;
        public string Detail
        {
            get
            {
                return detail;
            }
            set
            {
                detail = value;
                RaisePropertyChanged("Detail");
            }
        }

        private double cost = 0.0f;
        public double Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
                RaisePropertyChanged("Cost");
            }
        }

        private bool isChecked = false;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                RaisePropertyChanged("IsChecked");
            }
        }
        #endregion

        public TransactEntryVMBase()
        {
        }
    }
}