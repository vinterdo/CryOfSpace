using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryOfSpace
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

    class BuyOption : Option
    {
        public BuyOption(Component Item, int Price)
        {
            this.Item = Item;
            this.Price = Price;
        }

        public override void OnClick()
        {
            if (GeneralManager.Singleton.CurrentPlayer.Money >= Price)
            {
                GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(Item);
                GeneralManager.Singleton.CurrentPlayer.Money -= Price;
                
            }
        }
    }

    class SellOption: Option
    {
        public SellOption(Component Item, int Price)
        {
            this.Item = Item;
            this.Price = Price;
        }

        public override void OnClick()
        {
            foreach (Component C in GeneralManager.Singleton.CurrentPlayer.ComponentsInventory)
            {
                
                if (C.Name == Item.Name)
                {
                    GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Remove(C);
                    GeneralManager.Singleton.CurrentPlayer.Money += Price;
                    break;
                }
            }
            
        }

    }

    abstract class Option
    {
        public Component Item;
        public int Price;

        public abstract void OnClick();
    }

}