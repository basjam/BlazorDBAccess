namespace DBAccess.Models
{
    public class Car
    {
        public enum Condition
        {
            NEW, USED, DAMAGED, REBUILT
        }
        public int UUID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public DateOnly Year { get; set; }
        public Condition condition { get; set; }
        public int Odo { get; set; }
        public Person Owner { get; set; }
    }
}