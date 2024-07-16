using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BagAssignment;

namespace BagAssignmentTest
{
    [TestClass]
    public class BagTests
    {
        [TestMethod]
        public void TestAdd()
        {
            //a) Adding a non - null element to an empty bag
            Bag bag1 = new Bag();
            bag1.Insert(1);
            Assert.AreEqual(1, bag1.Frequency(1));

            //b) Adding a non - null element to a non-empty bag
            Bag bag2 = new Bag(new List<int> { 1, 2 });
            bag2.Insert(1);
            Assert.AreEqual(2, bag2.Frequency(1));

            //c) Adding a null element to an empty bag
            Bag bag3 = new Bag();
            Assert.ThrowsException<BagAssignment.ElementNullException>(() => bag3.Insert(null));

            //d) Adding a null element to a non-empty bag
            Bag bag4 = new Bag(new List<int> { 1, 2 });
            try
            {
                bag4.Insert(null); // add a null element
            }
            catch (BagAssignment.ElementNullException ex)
            {
                Console.WriteLine("An exception occurred: " + ex.Message);
            }

            //e) Inserting a non - integer element into a non-empty bag
            try
            {
                bag4.Insert('a'); // add a non-integer element
            }
            catch (BagAssignment.ElementNotIntegerException ex)
            {
                Console.WriteLine("An exception occurred: " + ex.Message);
            }
        }

        [TestMethod]
        public void TestRemove()
        {
            //a) Removing an element that exists in the bag
            Bag bag1 = new Bag(new List<int> { 1, 2, 2 });

            var result1 = bag1.Remove(2);

            Assert.IsTrue(result1);
            Assert.AreEqual(1, bag1.Frequency(2));

            //b) Removing an element that does not exist in the bag
            Bag bag2 = new Bag(new List<int> { 1, 2 });
            try
            {
                bag2.Remove(3);
            }
            catch (BagAssignment.ElementNotFoundException ex)
            {
                Console.WriteLine("An exception occurred: " + ex.Message);
            }

            //c) Removing the last occurrence of an element
            Bag bag3 = new Bag(new List<int> { 1, 2, 2 });
            var result3 = bag3.Remove(2);
            Assert.IsTrue(result3);
            Assert.AreEqual(1, bag3.Frequency(2));

            //d) Removing the only occurrence of an element
            Bag bag4 = new Bag(new List<int> { 1 });
            var result4 = bag4.Remove(1);
            Assert.IsTrue(result4);
            Assert.AreEqual(0, bag4.Frequency(1));

            //e) Removing a null element
            Bag bag5 = new Bag(new List<int> { 1, 2 });
            try
            {
                bag5.Remove(null); // add a null element
            }
            catch (BagAssignment.ElementNullException ex)
            {
                Console.WriteLine("An exception occurred: " + ex.Message);
            }
        }

        [TestMethod]
        public void TestFrequency()
        {
            //a) Getting the frequency of an element that exists in the bag
            Bag bag1 = new Bag(new List<int> { 1, 2, 3, 3, 3 });
            Assert.AreEqual(3, bag1.Frequency(3));

            //b) Getting the frequency of an element that does not exist in the bag
            Bag bag2 = new Bag(new List<int> { 1, 2 });
            try
            {
                bag2.Frequency(3);
            }
            catch (BagAssignment.ElementNotFoundException ex)
            {
                Console.WriteLine("An exception occurred: " + ex.Message);
            }

            //c) Getting the frequency of the last occurrence of an element
            Bag bag3 = new Bag(new List<int> { 1, 2, 3, 3, 3 });
            Assert.AreEqual(3, bag3.Frequency(3));

            //d) Getting the frequency of the only occurrence of an element
            Bag bag4 = new Bag(new List<int> { 1 });
            Assert.AreEqual(1, bag4.Frequency(1));

            //e) Getting the frequency of a null element
            Bag bag5 = new Bag(new List<int> { 1, 2 });
            try
            {
                bag5.Frequency(null); // add a null element
            }
            catch (BagAssignment.ElementNullException ex)
            {
                Console.WriteLine("An exception occurred: " + ex.Message);
            }
        }

        [TestMethod]
        public void TestSingleElementCount()
        {
            //a) Getting the count of single occurrence elements in an empty bag
            Bag bag1 = new Bag();
            Assert.AreEqual(0, bag1.SingleElementCount());

            //b) Getting the count of single - occurrence elements in a bag with no single - occurrence elements
            Bag bag2 = new Bag(new List<int> { 1, 1, 2, 2 });
            Assert.AreEqual(0, bag2.SingleElementCount());

            //c) Getting the count of single - occurrence elements in a bag with multiple single - occurrence elements
            Bag bag3 = new Bag(new List<int> { 1, 2, 3, 3, 3, 4, 5, 5 });
            Assert.AreEqual(3, bag3.SingleElementCount());

            //d) Getting the count of single occurrence elements in a bag with only one element that occurs more than once
            Bag bag4 = new Bag(new List<int> { 1, 1, 1 });
            Assert.AreEqual(0, bag4.SingleElementCount());
        }

        [TestMethod]
        public void TestPrint()
        {
            //a) Printing an empty bag
            Bag bag1 = new Bag();
            var output = new StringWriter();
            Console.SetOut(output);
            bag1.Print();
            Assert.AreEqual("", output.ToString());

            //b) Printing a bag with one element
            Bag bag2 = new Bag(new List<int> { 1 });
            output = new StringWriter();
            Console.SetOut(output);
            bag2.Print();
            Assert.AreEqual($"1: 1{Environment.NewLine}", output.ToString());

            //c) Printing a bag with multiple elements
            Bag bag3 = new Bag(new List<int> { 1, 2, 2, 3, 3, 3 });
            output = new StringWriter();
            Console.SetOut(output);
            bag3.Print();
            Assert.AreEqual($"1: 1{Environment.NewLine}2: 2{Environment.NewLine}3: 3{Environment.NewLine}", output.ToString());

            //d) Printing a bag with only single-occurrence elements
            Bag bag4 = new Bag(new List<int> { 1, 2, 3, 4, 5 });
            output = new StringWriter();
            Console.SetOut(output);
            bag4.Print();
            Assert.AreEqual($"1: 1{Environment.NewLine}2: 1{Environment.NewLine}3: 1{Environment.NewLine}4: 1{Environment.NewLine}5: 1{Environment.NewLine}", output.ToString());

            // e) Printing a bag with no single occurrence elements
            Bag bag5 = new Bag(new List<int> { 1, 1, 2, 2 });
            output = new StringWriter();
            Console.SetOut(output);
            bag5.Print();
            Assert.AreEqual($"1: 2{Environment.NewLine}2: 2{Environment.NewLine}", output.ToString());
        }
    }
}