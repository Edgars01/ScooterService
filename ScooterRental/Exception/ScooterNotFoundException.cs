namespace Scooters.Exception
{
    public class ScooterNotFoundException : System.Exception
    {
        public ScooterNotFoundException() : base(message:"Scooter not found")
        {

        }
    }
}
