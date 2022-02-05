using System;

namespace Scooters
{
    public class RentalCalculator : IRentalCalculator
    {
        public decimal CalculateRent(RentedScooter scooter)
        {
            var time = scooter.RentEnded - scooter.RentStarted;
            return Math.Round((decimal) time.Value.TotalMinutes * scooter.Price, MidpointRounding.ToEven);
        }
    }
}
