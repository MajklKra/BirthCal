using System.ComponentModel.DataAnnotations;

namespace BirthCal.Models
{
    public class Present
    {
        public int Id { get; set; }
        
        public string Description { get; set; }
        
        public int Price { get; set; }
        
        public string? URL { get; set; }
        
        public string? Note { get; set; }

        public DateTime Purchase { get; set; }

    }
}
