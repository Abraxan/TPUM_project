using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitBay3.Common
{
    public enum MessageTypes
    {
        WalletState,
        MarketState,
        SellConfirm,
        PurchaiseConfirm,
        Setup
    }
    
    public class Message
    {
        public MessageTypes Type { get; set; }
        public List<Entry> Data { get; set; }
        public string Text { get; set; }
    }
}
