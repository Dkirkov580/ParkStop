using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingStop.Data
{
    public class ParkingPlace
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public int Size { get; set; }
        public int CategoryId { get; set; }
        public Category Categories { get; set; }

        public decimal Price { get; set; }

        //[NotMapped]
        public Statuss Status { get; set; }
        public IsInvalid IsInvalid { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public DateTime RegisterOn { get; set; }
    }
}
