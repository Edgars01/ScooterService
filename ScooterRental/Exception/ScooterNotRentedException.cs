namespace Scooters.Exception
{
    public class ScooterNotRentedException : System.Exception
    {
        public ScooterNotRentedException() : base("Scooter not rented.")
        {

        }
    }
}
