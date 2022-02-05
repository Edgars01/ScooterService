using System;
using System.Collections.Generic;
using System.Linq;
using Scooters.Exception;

namespace Scooters
{
    public class ScooterService : IScooterService
    {
        private List<Scooter> _scooters;

        public ScooterService()
        {
            _scooters = new List<Scooter>();
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (pricePerMinute <= 0)
                throw new InvalidPriceException();
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("Invalid Id provided.");
            if (_scooters.Any(s => s.Id == id))
                throw new DuplicateScooterException();

            _scooters.Add(new Scooter(id, pricePerMinute));
        }

        public void RemoveScooter(string id)
        {
            if (_scooters.All(s => s.Id != id))
                throw new ScooterNotFoundException();

            _scooters.Remove(_scooters.First(s => s.Id == id));
        }

        public IList<Scooter> GetScooters()
        {
            return _scooters.ToList();
        }

        public Scooter GetScooterById(string scooterId)
        {
            var scooter = _scooters.FirstOrDefault(s => s.Id == scooterId);
            if (scooter == null)
                throw new ScooterNotFoundException();

            return scooter;
        }
    }
}
