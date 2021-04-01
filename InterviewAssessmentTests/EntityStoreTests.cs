using System;
using DomainModelEditor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewAssessmentTests
{
    [TestClass()]
    public class EntityStoreTests
    {
        [TestMethod()]
        public void LoadTest()
        {
            var entityStore = new EntityStore();

            var testData = new[] { new Tuple<int, string, int, int>(3, "Person", 150, 120), new Tuple<int, string, int, int>(4, "Employee", 20, 400) };
            entityStore.Load(testData);

            Assert.AreEqual(2, entityStore.Count);
            Assert.AreEqual(3, entityStore[0].Id);
            Assert.AreEqual(4, entityStore[1].Id);
            Assert.AreEqual("Employee", entityStore[1].Name);
            Assert.AreEqual(20, entityStore[1].X);
            Assert.AreEqual(400, entityStore[1].Y);
        }

        [TestMethod()]
        public void AddTest()
        {
            var entityStore = new EntityStore();

            entityStore.Add("Shape", 111, 222);

            Assert.AreEqual(1, entityStore.Count);
            Assert.AreEqual(1, entityStore[0].Id);
            Assert.AreEqual("Shape", entityStore[0].Name);
            Assert.AreEqual(111, entityStore[0].X);
            Assert.AreEqual(222, entityStore[0].Y);
        }
    }
}