using System.ComponentModel.DataAnnotations;

namespace MVC_ArtFlowAWS.Models
{
    public class Flower
    {
        [Key]
        public int ArtID { get; set; }
        public string? ArtName { get; set; }
        public string? ArtType { get; set; }
        public DateTime DateRequested { get; set; }
        public decimal ArtPrice { get; set; }

    }
}
