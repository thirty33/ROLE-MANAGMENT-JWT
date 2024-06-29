using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPeople.Models
{
    public class Task
    {
        //[Key]
        public Guid TaskId { get; set; }

        //[ForeignKey("CategoryId")]
        public Guid CategoryId { get; set; }

        //[Required]
        //[MaxLength(200 )]
        public string Tittle { get; set; }

        public string Description { get; set; }


        public Priority Priority { get; set; }

        public DateTime created_at { get; set; }

        public virtual Category Category { get; set; }

        //[NotMapped]
        public string Summary { get; set; }

    }

    public enum Priority
    {
        Low,
        Middle,
        High
    }
}
