namespace DBAccess.Models
{
    public class Person
    {
        public Guid UUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Car>? Cars { get; set; }

    }
}