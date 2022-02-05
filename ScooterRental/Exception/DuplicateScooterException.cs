namespace Scooters.Exception
{
    public class DuplicateScooterException : System.Exception
    {
        public DuplicateScooterException() : base("Scooter with provided id already exists.")
        {

        }
    }
}
