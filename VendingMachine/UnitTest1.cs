using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachine
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void ShouldReturnNothingIfEmpty()
        {
            var vendingMachine = new VendingMachine();
            var result = vendingMachine.Deliver(Choice.Cola);
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void ShouldReturnACan()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola);
            var can = vendingMachine.Deliver(Choice.Cola);
            Assert.IsNotNull(can);
        }

        [TestMethod]
        public void ShouldReturnACanOfTheSelectedChoice()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Fanta);
            vendingMachine.AddChoice(Choice.Cola);
            var can = vendingMachine.Deliver(Choice.Fanta);
            Assert.AreEqual(Choice.Fanta, can.Type);
        }

        [TestMethod]
        public void ShouldAcceptStockOfProduct()
        {
            //Given
            var vendingMachine = new VendingMachine();
            //When
            var accept = vendingMachine.AcceptStockOfProduct(Choice.Cola, 20);
            //Then
            Assert.AreEqual(true, accept);
        }

        [TestMethod]
        public void ShouldNotAcceptStockOfProduct_WhenQuantityIsGreaterThenMaxQuantity()
        {
            //Given
            var vendingMachine = new VendingMachine();
            //When
            var accept = vendingMachine.AcceptStockOfProduct(Choice.Cola, 30);
            //Then
            Assert.AreEqual(false, accept);
        }

        [TestMethod]
        public void AcceptStockOfProduct_ShouldNotAcceptNegativeQuantity()
        {
            //Given
            var vendingMachine = new VendingMachine();
            //When
            var accept = vendingMachine.AcceptStockOfProduct(Choice.Cola, -1);
            //Then
            Assert.AreEqual(false, accept);
        }

        [TestMethod]
        public void AcceptPriceOfProduct_WhenPriceIsPositive()
        {
            //Given
            var vendingMachine = new VendingMachine();
            //When
            var accept = vendingMachine.AcceptPriceOfProduct(Choice.Cola, 5);
            //Then
            Assert.AreEqual(true, accept);
        }

        [TestMethod]
        public void ShouldNot_AcceptPriceOfProduct_WhenPriceIsNegative()
        {
            //Given
            var vendingMachine = new VendingMachine();
            //When
            var accept = vendingMachine.AcceptPriceOfProduct(Choice.Cola, -1);
            //Then
            Assert.AreEqual(false, accept);
        }
    }

    public enum Choice
    {
        Cola,
        Fanta
    }

    public class VendingMachine
    {
        private readonly int _maxQuantity = 25;

        private readonly Dictionary<Choice, float> _priceDictionary = new Dictionary<Choice, float>()
        {
            { Choice.Cola, 2.5f },
            { Choice.Fanta, 3.5f },
        };
        private readonly List<Choice> _choices = new List<Choice>();

        public Can Deliver(Choice choice)
        {
            if (!_choices.Contains(choice))
            {
                return null;
            }
            return new Can { Type = choice };
        }

        public void AddChoice(Choice choice)
        {
            _choices.Add(choice);
        }

        public bool AcceptStockOfProduct(Choice choice, int quantity)
        {
            if (quantity < 0)
            {
                return false;
            }
            return quantity <= _maxQuantity;
        }


        public bool AcceptPriceOfProduct(Choice choice, float price)
        {
            if (price > 0)
            {
                _priceDictionary[choice] = price;
                return true;
            }
            return false;
        }
    }

    public class Can
    {
        public Choice Type { get; set; }
    }
}
