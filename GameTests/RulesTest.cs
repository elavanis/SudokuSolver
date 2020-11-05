using System;
using System.Collections.Generic;
using Game;
using Game.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameTests
{
    [TestClass]
    public class RulesTest
    {
        Mock<ICell> cell;
        List<ICell> neighbors;
        Rules rules;

        [TestInitialize]
        public void Setup()
        {
            cell = new Mock<ICell>();
            neighbors = new List<ICell>();
            rules = new Rules();

            cell.Setup(e => e.PossibleValues).Returns(new List<int>());
        }

        [TestMethod]
        public void Rules_SetWhenOnly1ValueLeft()
        {
            cell.Setup(e => e.PossibleValues).Returns(new List<int>() { 1 });

            RulesResult rulesResult = rules.Process(cell.Object, neighbors);

            Assert.AreEqual(1, rulesResult.Value);
            Assert.AreEqual(0, rulesResult.RemovedPossible.Count);
        }

        [TestMethod]
        public void Rules_RemoveOtherSetValues()
        {
            Mock<ICell> neighborCell = new Mock<ICell>();
            neighborCell.Setup(e => e.Value).Returns(1);
            neighbors.Add(neighborCell.Object);

            RulesResult rulesResult = rules.Process(cell.Object, neighbors);

            cell.Verify(e => e.RemovePossibleValue(1), Times.Once);
        }

        [TestMethod]
        public void Rules_FindOnlyValue()
        {
            cell.Setup(e => e.PossibleValues).Returns(new List<int>() { 1,3 });
            Mock<ICell> neighborCell = new Mock<ICell>();
            neighborCell.Setup(e => e.PossibleValues).Returns(new List<int>() { 2 });
            neighbors.Add(neighborCell.Object);

            RulesResult rulesResult = rules.Process(cell.Object, neighbors);

            Assert.AreEqual(1, rulesResult.Value);
            Assert.AreEqual(0, rulesResult.RemovedPossible.Count);
        }
    }
}
