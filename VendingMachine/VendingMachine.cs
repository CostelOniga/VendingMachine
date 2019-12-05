using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CreditCardModule;

namespace VendingMachine
{
    public class VendingMachine
    {
        private Dictionary<int,int> _stock = new Dictionary<int,int>();
        private double total;
        private double _colaPrice;
        private Dictionary<int, double> _prices = new Dictionary<int, double>();
        private CreditCard _cc;
        private bool _valid;
        private int ccc;

        public double T { get { return total; } }

        public Can Deliver(int option)
        {
            var price = _prices.ContainsKey(option) ? _prices[option] : 0;
            if (!_stock.ContainsKey(option) || IsInStock(option) || total < price)
            {
                return null;
            }

            _stock[option] = _stock[option] - 1;
            total -= price;
            return new Can { Type = option };
        }

        public void AddChoice(int choice, int count = int.MaxValue)
        {
            _stock.Add(choice, count);
        }
        public void AddMultipleChoices(int[] choices, int[] counts)
        {
            for (int i = 0; i < choices.Length; i++)
            {
                _stock.Add(choices[i], counts[i]);
            }
        }

        public void AddCoin(int v)
        {
            total += v;
        }

        public double Change()
        {
            var v = total;
            total = 0;
            return v;
        }

        public void AddPrice(int i, double v)
        {
            _prices[i] = v;
        }

        public void AddNewProduct(int choice, int quantity, double price)
        {
            _stock.Add(choice, quantity);
            _prices[choice] = price;
        }


        public double GetPrice(int choice)
        {
            return _prices[choice];
        }

        public void AcceptCard(CreditCard myCC)
        {
            _cc = myCC;
        }

        public void GetPinNumber(int pinNumber)
        {
            _valid = new CreditCardModule.CreditCardModule(_cc).HasValidPinNumber(pinNumber);
        }

        public void SelectChoiceForCard(int choice)
        {
            ccc = choice;
        }

        public Can DeliverChoiceForCard()
        {
            var choice = ccc;
            if (_valid && _stock[choice] > 0)
            {
                _stock[choice] = _stock[choice]-1;
                return new Can {Type = choice};
            }
            else
            {
                return null;
            }
        }
       
        private bool IsInStock(int option)
        {
            return _stock[option] < 1;
        }

    }
}