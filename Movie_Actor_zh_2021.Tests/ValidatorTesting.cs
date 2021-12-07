using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using Movie_Actor_zh_2021.App;

namespace Movie_Actor_zh_2021.Tests
{
    [TestFixture]
    [TestFixture]
    public class ValidatorTesting
    {
        public Dummy Dummy { get; set; }

        [SetUp]
        public void Setup()
        {
            this.Dummy = new Dummy();
        }

        [TestCase("1")]
        [TestCase("2")]
        public void TestValidatorWithValidData(string input)
        {
            // Arrange
            this.Dummy.Range = input;
            this.Dummy.Id = 0;

            // Act
            bool isValid = this.Dummy.IsValid(nameof(Dummy.Range));

            // Assert
            NUnit.Framework.Assert.IsTrue(isValid);
        }

        [TestCase("test")]
        [TestCase("")]
        public void TestValidatorWithInvalidData(string input)
        {
            // Arrange
            this.Dummy.Range = input;
            this.Dummy.Id = 9;

            // Act
            bool isValid = this.Dummy.IsValid(nameof(Dummy.Range));

            // Assert
            NUnit.Framework.Assert.IsFalse(isValid);
        }

        [TestCase("1")]
        [TestCase("2")]
        public void TestValidatorArgumentException(string input)
        {
            // Arrange
            this.Dummy.Range = input;
            this.Dummy.Id = 3;

            // Act
            // Assert
           Throws<System.ArgumentException>(() => this.Dummy.IsValid(nameof(Dummy.Id)));
        }
    }

    public class Dummy
    {
        public int Id { get; set; }

        [StringRange("1", "2", "3")]
        public string Range { get; set; }
    }
}
