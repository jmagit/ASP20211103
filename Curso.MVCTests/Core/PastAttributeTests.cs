using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curso.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Curso.Domains.Entities;

namespace Curso.Core.Tests {
    [TestClass()]
    public class PastAttributeTests {

        [TestMethod()]
        public void IsValidWhenValidTest() {
            // var c = new Customer() { ModifiedDate = DateTime.Now.AddMinutes(-1) };
            var attr = new PastAttribute();
            Assert.IsTrue(attr.IsValid(DateTime.Now.AddMinutes(-1)));
        }
        [TestMethod()]
        public void IsValidWhenNullTest() {
            // var c = new Customer() { ModifiedDate = DateTime.Now.AddMinutes(-1) };
            var attr = new PastAttribute();
            Assert.IsTrue(attr.IsValid(null));
        }
        [TestMethod()]
        public void IsValidWhenInValidTest() {
            // var c = new Customer() { ModifiedDate = DateTime.Now.AddMinutes(-1) };
            var attr = new PastAttribute();
            Assert.IsFalse(attr.IsValid(DateTime.Now.AddMinutes(1)));
        }
        [TestMethod()]
        public void IsValidWhenNoDatetimeTest() {
            // var c = new Customer() { ModifiedDate = DateTime.Now.AddMinutes(-1) };
            var attr = new PastAttribute();
            Assert.IsFalse(attr.IsValid(1));
        }
    }
}