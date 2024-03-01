using System.ComponentModel.DataAnnotations;

namespace Eco.Models
{
    public class Product
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }   


        public string ImageURL { get; set; }


        public ICollection<ChartItem> ChartItems { get; set; }
    }
}
