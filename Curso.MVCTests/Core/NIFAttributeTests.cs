using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curso.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Curso.Core.Tests {
    [TestClass()]
    public class NIFAttributeTests {
        class NifMock : NIFAttribute {
            public new ValidationResult IsValid(object value) {
                return base.IsValid(value, null);
            }
        }

        NifMock attr = new NifMock();

        [TestMethod()]
        [DataRow("12345678z")]
        [DataRow("12345678Z")]
        [DataRow("1234S")]
        [DataRow(null)]
        public void NIFValidTest(string nif) {
            Assert.AreEqual(ValidationResult.Success, attr.IsValid(nif));
        }

        [TestMethod()]
        [DataRow("12345678")]
        [DataRow("Z")]
        [DataRow("1234J")]
        [DataRow("")]
        public void NIFInvalidTest(string nif) {
            var rslt = attr.IsValid(nif);
            Assert.AreNotEqual(ValidationResult.Success, rslt);
            Assert.AreEqual("No es un NIF válido.", rslt.ErrorMessage);
        }
    }


}