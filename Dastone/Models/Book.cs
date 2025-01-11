using System.ComponentModel.DataAnnotations.Schema;

namespace Dastone.Models
{
    public class Book
    {
     
        public int BookId { get; set; }
        public string BookName { get; set; }
        public decimal Price { get; set; }
    }
}
