using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scooters.Exception;

namespace Scooters
{
    public class RentalCompany : IRentalCompany
    {

        public RentalCompany(string name, 
            IScooterService serice, 
            IList<RentedScooter> archive,
            IRentalCalculator calculator)
        {
            Name = name ?? throw new InvalidCompanyNameException();
            _scooterService = serice;
            _rentedScooters = archive;
            _calculator = calculator;
        }

        private readonly IScooterService _scooterService;
        private readonly IRentalCalculator _calculator;
        private readonly IList<RentedScooter> _rentedScooters;

       public string Name { get; }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            throw new NotImplementedException();
        }

        public decimal EndRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            if (!scooter.IsRented) 
                throw new ScooterNotRentedException();
             
            scooter.IsRented = false;
            var rented = _rentedScooters
                .First(s => s.Id == id && !s.RentEnded.HasValue);
            rented.RentEnded = DateTime.UtcNow;

            return _calculator.CalculateRent(rented);
        }

        public void StartRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            if (scooter.IsRented)
                throw new ScooterRentedException();
            
            scooter.IsRented = true;

            _rentedScooters.Add(new RentedScooter
            {
                Id = scooter.Id,
                Price = scooter.PricePerMinute,
                RentStarted = DateTime.UtcNow
            });
        }
    }
}
