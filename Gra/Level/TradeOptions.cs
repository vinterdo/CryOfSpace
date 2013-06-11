using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gra
{
    class TradeOptions
    {
        public List<BuyOption> Buy;
        public List<SellOption> Sell;

        public TradeOptions()
        {
            Buy = new List<BuyOption>();
            Sell = new List<SellOption>();
        }

        public void AddBuyOption(BuyOption Opt)
        {
            Buy.Add(Opt);
        }

        public void AddSellOption(SellOption Opt)
        {
            Sell.Add(Opt);
        }
    }

    class BuyOption
    {
        public BuyOption(Component Item, int Price)
        {
            this.Item = Item;
            this.Price = Price;
        }

        Component Item;
        int Price;
    }

    class SellOption
    {
        public SellOption(Component Item, int Price)
        {
            this.Item = Item;
            this.Price = Price;
        }

        Component Item;
        int Price;
    }

}
