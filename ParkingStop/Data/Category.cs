namespace ParkingStop.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<ParkingPlace> ParkingPlaces { get; set; }
    }
}
