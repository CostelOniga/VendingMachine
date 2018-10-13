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
    }

    public enum Choice
    {
        Cola,
        Fanta
    }

    public class VendingMachine
    {
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
    }

    public class Can
    {
        public Choice Type { get; set; }
    }
}
