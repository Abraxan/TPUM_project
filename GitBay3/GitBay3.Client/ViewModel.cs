using GitBay3.Client.Logic;
using GitBay3.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GitBay3.Client
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Model _model;

        public ObservableCollection<Entry> Wallet
        {
            get
            {
                return _model.Wallet;
            }
            set
            {
                _model.Wallet = value;
                OnPropertyChanged("Wallet");
            }
        }

        public ObservableCollection<Entry> Market
        {
            get
            {
                return _model.Market;
            }
            set
            {
                _model.Market = value;
                OnPropertyChanged("Market");
            }
        }

        public ViewModel()
        {
            _model = new Model();
            //LoadAccount();
        }

        public void Buy(Currencies name, float amount)
        {
            _model.Buy(name, amount);
        }

        public void Sell(Currencies name, float amount)
        {
            _model.Sell(name, amount);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, e);
            }
        }

        public void LoadAccount()
        {
            _model.LoadAccount();
        }

        public void Close()
        {
            _model.Close();
        }
    }
}
