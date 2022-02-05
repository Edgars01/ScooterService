using System;
using NUnit.Framework;
using Scooters;
using Scooters.Exception;

namespace ScootersTests
{
    public class ScooterServiceTests
    {
        private IScooterService _target;

        [SetUp]
        public void Setup()
        {
            _target = new ScooterService();
        }

        [Test]
        public void AddScooter_1_020_ScooterAdded()
        {   // Act
            _target.AddScooter("1", 0.20m);
            // Assert
            Assert.AreEqual(1, _target.GetScooters().Count);
        }

        [Test]
        public void AddScooter_1_NegativePrice_ShouldFail()
        {   // Act
           Assert.Throws<InvalidPriceException>( () => _target.AddScooter("1", -0.20m));
        }

        [Test]
        public void AddScooter_DuplicateId_ShouldFail()
        {   
            // Arrange
            var id = "1";
            _target.AddScooter(id, 0.20m);
            // Assert
            Assert.Throws<DuplicateScooterException>(() => _target.AddScooter(id, 0.20m));
        }

        [Test]
        public void AddScooter_1_GetSameScooterBack()
        {
            // Arrange
            var id = "1";
            _target.AddScooter(id, 0.20m);
            // Act
            var scooter = _target.GetScooterById(id);
            // Assert
            Assert.AreEqual(id, scooter.Id);
        }


        [Test]
        public void AddScooter_NullID_ShouldFail()
        {   // Act
            Assert.Throws<ArgumentException>(() => _target.AddScooter(null, 0.20m));
        }

        [Test]
        public void RemoveScooter_NotExisting_ScooterFail()
        {
            // Arrange
            var id = "1";
            
            // Assert
            Assert.Throws<ScooterNotFoundException>(() => _target.RemoveScooter(id));
        }

        [Test]
        public void RemoveScooter_1_020_ScooterRemoved()
        {
            // Arrange
            var id = "1";
            var pricePerMinute = 0.20m;
            _target.AddScooter(id, pricePerMinute);

            // Act
            _target.RemoveScooter("1");

            // Assert
            Assert.Throws<ScooterNotFoundException>(() => _target.GetScooterById(id));
        }

        
        [Test]
        public void GetScooters_ChangeInventoryWithoutService_ShouldFail()
        {
            // Arrange
            var scooters = _target.GetScooters();
            
            scooters.Add(new Scooter("1", 0.20m));
            scooters.Add(new Scooter("2", 0.20m));
            scooters.Add(new Scooter("3", 0.20m));

            // Assert
            Assert.AreEqual(0, _target.GetScooters().Count);
        }
    }
}