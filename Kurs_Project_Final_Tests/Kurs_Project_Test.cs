using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kurs_Project_Final;

namespace Kurs_Project_Final_Tests
{
    [TestClass]
    public class Kurs_Project_Test
    {
        [TestMethod]
        public void Decode_test()
        {
            // arrange
            string text = "University Кфор,"; 
            // act  
            Program.Decode(text.Length, text, out string actual);
            // assert  
            string expected = "University Итмо,";
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Code_test()
        {
            // arrange
            string direction = "+";
            int q =1;
            string text = "АяZZ123";
            // act  
            Program.Сode(q, direction, text, out string actual);
            // assert  
            string expected = "БаZZ123";
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void Algorithm_test()
        {
            //arrange
            int q = 2;
            char letter = 'а';
            string direction = "-";
            // act  
            char actual = Program.Algorithm(q, letter , direction);
            // assert
            char expected = 'ю';
            Assert.AreEqual(actual, expected);
        }
    }
}
