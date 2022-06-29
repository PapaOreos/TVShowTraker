using System.ComponentModel.DataAnnotations;

namespace TVShowTraker.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }

        public bool IsValid()
        {
            if(Id <= 0) return false;
            return true;
        }
    }
}
