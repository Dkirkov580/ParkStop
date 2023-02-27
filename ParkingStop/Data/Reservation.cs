namespace ParkingStop.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime DateOn { get; set; }
        public DateTime DateOff { get; set; }
        public string RegNum { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public int ParkingPlaceId { get; set; }
        public ParkingPlace ParkingPlaces { get; set; }

        public string UserId { get; set; }
        public User Users { get; set; }

        public DateTime RegisterOn { get; set; }
    }
}
