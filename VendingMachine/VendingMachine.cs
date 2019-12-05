using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using CreditCardModule;

namespace VendingMachine
{
    public class VendingMachine
    {
        private List<int> _choices = new List<int>();
        private int[] _quantityKeys = {};
        private int[] _quantityValues = {};
        private double total;
        private double _colaPrice;
        private Dictionary<int, double> _prices = new Dictionary<int, double>();
        private CreditCard _cc;
        private bool _valid;
        private int ccc;

        public double T { get { return total; } }

        public Can Deliver(int value)
        {
            var price = _prices.ContainsKey(value) ? _prices[value] : 0;
            if (!_choices.Contains(value) || _quantityValues[Array.IndexOf(_quantityKeys, value)] < 1 || total < price)
            {
                return null;
            }

            _quantityValues[Array.IndexOf(_quantityKeys, value)] = _quantityValues[Array.IndexOf(_quantityKeys, value)]-1;
            total -= price;
            return new Can { Type = value };
        }

        public void AddChoice(int choice, int count = int.MaxValue)
        {
            AddToDictionary(choice, count);
        }
        public void AddMultipleChoices(int[] choices, int[] counts)
        {
            for (int i = 0; i < choices.Length; i++)
            {
                AddToDictionary(choices[i], counts[i]);
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

        public void Stock(int choice, int quantity, double price)
        {
            AddToDictionary(choice, quantity);
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
            if (_valid && _choices.IndexOf(choice) > -1 && _quantityValues[Array.IndexOf(_quantityKeys, choice)] > 0)
            {
                _quantityValues[Array.IndexOf(_quantityKeys, choice)] = _quantityValues[Array.IndexOf(_quantityKeys, choice)]-1;
                return new Can {Type = choice};
            }
            else
            {
                return null;
            }
        }
        private void AddToDictionary(int choice, int count)
        {
            Array.Resize(ref _quantityKeys, _quantityKeys.Length + 1);
            Array.Resize(ref _quantityValues, _quantityValues.Length + 1);
            _quantityKeys[_quantityKeys.Length - 1] = choice;
            _quantityValues[_quantityValues.Length - 1] = count;
            _choices.Add(choice);
        }

    }
}