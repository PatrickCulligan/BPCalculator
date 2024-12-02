using NUnit.Framework;
using BPCalculator;
using System;
using System.ComponentModel.DataAnnotations;

namespace BPCalculatorTests
{
    [TestFixture]
    public class BloodPressureTests
    {
        [Test]
        public void BloodPressure_Category_ShouldReturnLow_WhenSystolicAndDiastolicAreLow()
        {
            // Arrange
            var bp = new BloodPressure
            {
                Systolic = 85,
                Diastolic = 55
            };

            // Act
            var category = bp.Category;

            // Assert
            Assert.AreEqual(BPCategory.Low, category);
        }

        [Test]
        public void BloodPressure_Category_ShouldReturnIdeal_WhenValuesAreWithinIdealRange()
        {
            // Arrange
            var bp = new BloodPressure
            {
                Systolic = 115,
                Diastolic = 75
            };

            // Act
            var category = bp.Category;

            // Assert
            Assert.AreEqual(BPCategory.Ideal, category);
        }

        [Test]
        public void BloodPressure_Category_ShouldReturnPreHigh_WhenValuesAreWithinPreHighRange()
        {
            // Arrange
            var bp = new BloodPressure
            {
                Systolic = 130,
                Diastolic = 85
            };

            // Act
            var category = bp.Category;

            // Assert
            Assert.AreEqual(BPCategory.PreHigh, category);
        }

        [Test]
        public void BloodPressure_Category_ShouldReturnHigh_WhenValuesAreWithinHighRange()
        {
            // Arrange
            var bp = new BloodPressure
            {
                Systolic = 150,
                Diastolic = 95
            };

            // Act
            var category = bp.Category;

            // Assert
            Assert.AreEqual(BPCategory.High, category);
        }

      

    [Test]
        public void BloodPressure_ShouldNotThrowException_WhenValuesAreWithinRange()
        {
            // Arrange
            var bp = new BloodPressure
            {
                Systolic = 120,
                Diastolic = 80
            };

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var category = bp.Category;
            });
        }

       

        [Test]
        public void BloodPressure_ShouldReturnCorrectCategory_WhenDiastolicIsAtMaxBoundary()
        {
            // Arrange
            var bp = new BloodPressure
            {
                Systolic = 120,
                Diastolic = BloodPressure.DiastolicMax
            };

            // Act
            var category = bp.Category;

            // Assert
            Assert.AreEqual(BPCategory.High, category);
        }

        [Test]
        public void BloodPressure_Category_ShouldReturnPreHigh_WhenSystolicAndDiastolicInPreHighRange()
        {
            // Arrange
            var bp = new BloodPressure
            {
                Systolic = 135,
                Diastolic = 85
            };

            // Act
            var category = bp.Category;

            // Assert
            Assert.AreEqual(BPCategory.PreHigh, category);
        }

        

        [Test]
        public void BloodPressure_ShouldReturnIdealCategory_WhenValuesAreAtBoundaryOfIdealRange()
        {
            // Arrange
            var bp = new BloodPressure
            {
                Systolic = 120,
                Diastolic = 80
            };

            // Act
            var category = bp.Category;

            // Assert
            Assert.AreEqual(BPCategory.Ideal, category);
        }

        [Test]
        public void BloodPressure_Category_ShouldReturnHigh_WhenSystolicExceeds140()
        {
            // Arrange
            var bp = new BloodPressure
            {
                Systolic = 150,
                Diastolic = 95
            };

            // Act
            var category = bp.Category;

            // Assert
            Assert.AreEqual(BPCategory.High, category);
        }

        [Test]
        public void BloodPressure_Category_ShouldReturnLow_WhenBothSystolicAndDiastolicAreLow()
        {
            // Arrange
            var bp = new BloodPressure
            {
                Systolic = 80,
                Diastolic = 50
            };

            // Act
            var category = bp.Category;

            // Assert
            Assert.AreEqual(BPCategory.Low, category);
        }


    }
}

