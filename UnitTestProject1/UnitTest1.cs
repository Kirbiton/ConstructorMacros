﻿using ConstructorMacrosWPF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            bool Peremeniy = true;
            //Act
            bool Check = Page1.IsFirtstFocus(Peremeniy);
            //Assert
            Assert.AreEqual(Check, true);
        }
    }
}
