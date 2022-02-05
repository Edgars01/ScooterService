using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Scooters;
using Scooters.Exception;

namespace ScootersTests
{
    public class RentalCompanyTests
    {

        private IRentalCompany _target;
        private IScooterService _scooterService;
        private IList<RentedScooter> _rentedScooters;
        private IRentalCalculator _calculator;

        private string _id = "1";
        private decimal _defaultPrice = 0.20m;

        private string _defaultName => "Company";
        [SetUp]
        public void Setup()
        {
            _scooterService = new ScooterService();
            _rentedScooters = new List<RentedScooter>();
            _calculator = new RentalCalculator();
            _target = new RentalCompany(_defaultName,
                _scooterService,
                _rentedScooters,
                _calculator);
        }

        [Test]
        public void CreateCompany_DefaultName_ShouldReturnDefaultName()
        {
            //Arrange
            _target = new RentalCompany(_defaultName, _scooterService, _rentedScooters, _calculator);

            //Assert
            Assert.AreEqual(_defaultName, _target.Name);
        }

        [Test]
        public void CreateCompany_NullNameGiven_ThrowsInvalidNameException()
        {
            //Assert
            Assert.Throws<InvalidCompanyNameException>(() => _target = new RentalCompany(null, _scooterService, _rentedScooters, _calculator));
        }

        [Test]
        public void StartRent_RentFirstScooter_ScooterShouldBeRented()
        {
            // Arrange
            _scooterService.AddScooter(_id, _defaultPrice);

            // Act
            _target.StartRent(_id);

            // Assert   
            var scooter = _scooterService.GetScooterById(_id);
            Assert.AreEqual(true, scooter.IsRented);
        }

        [Test]
        public void StartRent_RentNonExistingScooter_ShouldThrowScooterNotFoundException()
        {
            // Assert   
            Assert.Throws<ScooterNotFoundException>(() => _target.StartRent(_id));
        }

        [Test]
        public void StartRent_FirstScooterRented_RentedListShouldBeUpdated()
        {
            // Arrange
            _scooterService.AddScooter(_id, _defaultPrice);

            // Act
            _target.StartRent(_id);

            // Assert
            var RentedScooter = _rentedScooters.FirstOrDefault(s => s.Id ==  _id);
            Assert.AreEqual(1, _rentedScooters.Count);
            Assert.AreEqual(_defaultPrice, RentedScooter.Price);
        }


        [Test]
        public void EndRent_EndingRentFirstScooter_ShouldPass()
        {
            // Arrange
            _scooterService.AddScooter(_id, _defaultPrice);
            _target.StartRent(_id);

            // Act
            var result = _target.EndRent(_id);

            // Assert   
            Assert.GreaterOrEqual(result, 0);
        }


        [Test]
        public void EndRent_EndingRentNotRentedScooter_ShouldThrowScooterNotRentedException()
        {
            // Arrange
            _scooterService.AddScooter(_id, _defaultPrice);

            // Assert   
            Assert.Throws<ScooterNotRentedException>( () => _target.EndRent(_id));
        }


        [Test]
        public void EndRent_FirstScooterRentEnded_RentedListShouldBeUpdated()
        {
            // Arrange
            _scooterService.AddScooter(_id, _defaultPrice);
            _target.StartRent(_id);

            // Act
            _target.EndRent(_id);

            // Assert
            var RentedScooter = _rentedScooters.FirstOrDefault(s => s.Id == _id);
            Assert.AreEqual(1, _rentedScooters.Count);
            Assert.NotNull(RentedScooter.RentEnded);
        }

        [Test]
        public void EndRent_ScooterRented1Day_ShouldReturn20()
        {
            // Arrange
            _scooterService.AddScooter(_id, 1);
            _scooterService.GetScooterById(_id).IsRented = true;
            var startRent = DateTime.UtcNow.AddMinutes(-20);
            _rentedScooters.Add(new RentedScooter
            {
                Id = _id,
                RentStarted = startRent,
                Price = 1
            });
            
            // Act
            var result = _target.EndRent(_id);

            // Assert
            Assert.AreEqual(20.0m, result);
        }
    }
}
