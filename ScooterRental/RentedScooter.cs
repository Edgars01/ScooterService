using System;

namespace Scooters
{
    public class RentedScooter
    {
        public string Id { get; set; }
        public decimal Price { get; set; }

        public DateTime RentStarted { get; set; }
        public DateTime ?RentEnded { get; set; }

    }
}
