namespace Scooters.Exception
{
    public class ScooterRentedException : System.Exception
    {
        public ScooterRentedException() : base("Scooter is already rented.")
        {

        }
    }
}
