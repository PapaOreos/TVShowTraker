using System.ComponentModel.DataAnnotations;

namespace TVShowTraker.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
